using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Power.Mvc.Helper
{
    /// <summary>
    /// 呼叫非同步方法，但以同步方式回傳結果
    /// </summary>
    public static class AsyncTaskHelper
    {
        /// <summary>
        /// Execute's an async Task method which has a void return value synchronously
        /// </summary>
        /// <param name="task">Task method to execute</param>
        public static void RunSync(Func<Task> task)
        {
            // 當前的同步上下文
            SynchronizationContext currentContext = SynchronizationContext.Current;

            // 新的額外的同步上下文
            ExclusiveSynchronizationContext synch = new ExclusiveSynchronizationContext();

            // 設定當前的同步上下文為新的額外的那一個，因為要對他做重寫的同步處理
            SynchronizationContext.SetSynchronizationContext(synch);
            synch.Post(
                async o =>
                {
                    try
                    {
                        await task();
                    }
                    catch (Exception e)
                    {
                        synch.InnerException = e;
                        throw;
                    }
                    finally
                    {
                        synch.EndMessageLoop();
                    }
                },
                null);

            synch.BeginMessageLoop();

            // 調整回原始的 context
            SynchronizationContext.SetSynchronizationContext(currentContext);
        }

        /// <summary>
        /// Execute's an async Task method which has a T return type synchronously
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="task">Task method to execute</param>
        /// <returns>Task result</returns>
        public static T RunSync<T>(Func<Task<T>> task)
        {
            SynchronizationContext currentContext = SynchronizationContext.Current;
            ExclusiveSynchronizationContext synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            T result = default(T);
            synch.Post(
                async o =>
                {
                    try
                    {
                        result = await task();
                    }
                    catch (Exception e)
                    {
                        synch.InnerException = e;
                        throw;
                    }
                    finally
                    {
                        synch.EndMessageLoop();
                    }
                },
                null);

            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(currentContext);

            return result;
        }

        /// <summary>
        /// 私用同步處理模式
        /// </summary>
        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            /// <summary>
            /// 向等候的執行緒通知發生事件，設定初始狀態為 false
            /// </summary>
            private readonly AutoResetEvent WorkItemsWaiting = new AutoResetEvent(false);

            /// <summary>
            /// A queue for SendOrPostCallback and state work items
            /// </summary>
            private readonly Queue<Tuple<SendOrPostCallback, object>> Items = new Queue<Tuple<SendOrPostCallback, object>>();

            /// <summary>
            /// Is work item method already done
            /// </summary>
            private bool IsDone;

            /// <summary>
            /// The exception threw by work item's callback method
            /// </summary>
            public System.Exception InnerException { private get; set; }

            /// <summary>
            /// 在衍生類別中覆寫時，會將同步訊息分派至同步處理內容。
            /// </summary>
            /// <param name="d">要呼叫的 <see cref="T:System.Threading.SendOrPostCallback" /> 委派。</param>
            /// <param name="state">傳送至委派的物件。</param>
            /// <exception cref="NotSupportedException"></exception>
            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("We cannot send to our same thread");
            }

            /// <summary>
            /// 在衍生類別中覆寫時，會將非同步訊息分派至同步處理內容。
            /// </summary>
            /// <param name="d">要呼叫的 <see cref="T:System.Threading.SendOrPostCallback" /> 委派。</param>
            /// <param name="state">傳送至委派的物件。</param>
            public override void Post(SendOrPostCallback d, object state)
            {
                // lock work items
                lock (this.Items)
                {
                    // push back work item to queue
                    this.Items.Enqueue(Tuple.Create(d, state));
                }

                // set state
                this.WorkItemsWaiting.Set();
            }

            /// <summary>
            /// 在衍生類別中覆寫時，會建立同步處理內容的複本。
            /// </summary>
            /// <returns>新的 <see cref="T:System.Threading.SynchronizationContext" /> 物件。</returns>
            public override SynchronizationContext CreateCopy()
            {
                return this;
            }

            /// <summary>
            /// Begin message
            /// </summary>
            /// <exception cref="AggregateException"></exception>
            public void BeginMessageLoop()
            {
                while (!this.IsDone)
                {
                    Tuple<SendOrPostCallback, object> task = null;

                    // lock work items
                    lock (this.Items)
                    {
                        if (this.Items.Count > 0)
                        {
                            task = this.Items.Dequeue();
                        }
                    }

                    // is task not null, then invoke it
                    if (task != null)
                    {
                        task.Item1(task.Item2);

                        // the method threw an exeption
                        if (this.InnerException != null)
                        {
                            throw new AggregateException("AsyncHelpers.Run method threw an exception.", this.InnerException);
                        }
                    }
                    else
                    {
                        this.WorkItemsWaiting.WaitOne();
                    }
                }
            }

            /// <summary>
            /// Set the end message to work item queue via Post
            /// </summary>
            public void EndMessageLoop()
            {
                this.Post(o => this.IsDone = true, null);
            }
        }
    }
}