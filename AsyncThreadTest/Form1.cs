using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncThreadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Private Method
        /// <summary>
        /// 一个比较耗时耗资源的私有方法
        /// </summary>
        /// <param name="name"></param>
        private void DoSomethingLong(string name)
        {
            Console.WriteLine($"****************DoSomethingLong Start {Thread.CurrentThread.ManagedThreadId}   {name}{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}***************");
            long lResult = 0;
            for (int i = 0; i < 10000000; i++)
            {
                lResult += i;
            }
            Thread.Sleep(2000);

            Console.WriteLine($"****************DoSomethingLong   End {Thread.CurrentThread.ManagedThreadId}{name}{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}***************");
        }
        #endregion
        /// <summary>
        /// 同步方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSync_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"同步方法开始{Thread.CurrentThread.ManagedThreadId}{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format($"btnSync_Click_{i}");
                DoSomethingLong(name);
            }
            Console.WriteLine($"同步方法结束{Thread.CurrentThread.ManagedThreadId}{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
        }
        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAsync_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"异步方法开始{Thread.CurrentThread.ManagedThreadId}");
            var act=new Action<string>(DoSomethingLong);
            //act.Invoke("buttonAsync_Click1");
            //act("buttonAsync_Click2");
            IAsyncResult iAsyncResult = null;
            AsyncCallback callback = t =>
            {Console.WriteLine(t);
                Console.WriteLine($"string.ReferenceEquals(t, iAsyncResult)={string.ReferenceEquals(t, iAsyncResult)}");
                Console.WriteLine($"this is callback{Thread.CurrentThread.ManagedThreadId}");
            };
            iAsyncResult= act.BeginInvoke("buttonAsync_Click", callback, "whx");
            Console.WriteLine("Do somethingelse");
            Console.WriteLine("Do somethingelse");
            Console.WriteLine("Do somethingelse");
            Console.WriteLine("Do somethingelse");
            //while (!iAsyncResult.IsCompleted)
            //{
            //    Thread.Sleep(10000);
            //  Console.WriteLine("继续等待");  
            //}
            iAsyncResult.AsyncWaitHandle.WaitOne();
            //iAsyncResult.AsyncWaitHandle.WaitOne(200);
            //act.EndInvoke(iAsyncResult);
            Console.WriteLine("异步操作完成后执行的任务1");
            Console.WriteLine("异步操作完成后执行的任务2");
            Console.WriteLine("异步操作完成后执行的任务3");
            Console.WriteLine("异步操作完成后执行的任务4");
            Console.WriteLine("异步操作完成后执行的任务5");
            Console.WriteLine($"异步方法结束{Thread.CurrentThread.ManagedThreadId}");
        }


        private void buttonAsyncThreds_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"****************btnAsyncThreads_Click Start {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}***************");

            Action<string> act = DoSomethingLong;
            for (var i = 0; i < 5; i++)
            {
                var nane = string.Format($"buttonAsyncThreds_Click{i}");
                act.BeginInvoke(nane, null, null);
            }

            watch.Stop();
            Console.WriteLine($"****************btnAsyncThreads_Click   End {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}  {watch.ElapsedMilliseconds}***************");

        }
        /// <summary>
        /// thread默认是前台线程,启动后计算完才能退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonThread_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //Thread
            Console.WriteLine($"****************btnThread_Click Start {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}***************");
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i < 5; i++)
            {
                var name = string.Format($"buttonThread_Click{i}");
                ThreadStart start = () => this.DoSomethingLong(name);
                Thread thread=new Thread(start);
                thread.Start();
                //thread.IsBackground = true;//设置为后台线程
                   threadList.Add(thread);
                //    //别用
                //    //thread.Suspend();//暂停
                //    //thread.Resume();//恢复
                //    //thread.Abort();//销毁线程
                //    //停止线程  靠的不是外部力量，而是线程自身，外部修改信号量，线程检测信号量
                //    //thread.Join();//线程等待
            }
            foreach (var thread in threadList)
            {
                thread.Join();//表示把thread线程任务join到当前线程，也就是当前线程等着thread任务完成

            }

            watch.Stop();//能正确统计全部耗时
            Console.WriteLine($"****************btnThread_Click   End {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}  {watch.ElapsedMilliseconds}***************");
        }
        /// <summary>
        /// 2.0 线程池  享元模式   单例模式//避免重复的申请和销毁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonThreadpool_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"****************btnThreadPool_Click Start {Thread.CurrentThread.ManagedThreadId}***************");

            var mre=new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(t =>
            {
                DoSomethingLong("buttonThreadpool_Click");
                mre.Set();
            });
            mre.WaitOne();
            //ThreadPool.QueueUserWorkItem(t =>
            //{
            //    Console.WriteLine(t);
            //    Thread.Sleep(2000);
            //    Console.WriteLine($"这里是线程池{Thread.CurrentThread.ManagedThreadId}");
            //});
            //ThreadPool.QueueUserWorkItem(t =>
            //{
            //    Console.WriteLine(t);
            //    Thread.Sleep(2000);
            //    Console.WriteLine($"这里是线程池{Thread.CurrentThread.ManagedThreadId}");
            //},"whx");

            //#region PoolSet
            //ThreadPool.SetMaxThreads(8, 8);//最小也是核数
            //ThreadPool.SetMinThreads(8, 8);
            //int workerThreads = 0;
            //int ioThreads = 0;
            //ThreadPool.GetMaxThreads(out workerThreads, out ioThreads);
            //Console.WriteLine(String.Format("Max worker threads: {0};    Max I/O threads: {1}", workerThreads, ioThreads));

            //ThreadPool.GetMinThreads(out workerThreads, out ioThreads);
            //Console.WriteLine(String.Format("Min worker threads: {0};    Min I/O threads: {1}", workerThreads, ioThreads));

            //ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
            //Console.WriteLine(String.Format("Available worker threads: {0};    Available I/O threads: {1}", workerThreads, ioThreads));
            //#endregion
            Console.WriteLine($"****************btnThreadPool_Click   End {Thread.CurrentThread.ManagedThreadId}***************");

        }
        /// <summary>
        /// TASK：基于线程池线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTask_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"****************btnTask_Click Start {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}***************");
            var list=new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format($"btnTask_Click_{i}");
               var task= Task.Factory.StartNew(() => this.DoSomethingLong(name));
                list.Add(task);
            }
            Task.Factory.ContinueWhenAll(list.ToArray(),
                tlist =>
                {
                    Console.WriteLine(tlist[0].IsCompleted);
                    Console.WriteLine($"continuewhenall{Thread.CurrentThread.ManagedThreadId}");
                });
            //Console.WriteLine("Before WaitAll");
            ////Task.WaitAll(list.ToArray());//等待当前线程全部完成，很关键包括前面所有的等待都会卡ui主线程

            //Task.WaitAny(list.ToArray());
            //Console.WriteLine("After WaitAll");
            watch.Stop();
            Console.WriteLine($"****************btnTask_Click   End {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}  {watch.ElapsedMilliseconds}***************");
        }
        /// <summary>
        /// 并行编程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonParallel_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"****************btnParallel_Click Start {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}***************");
            //Parallel.Invoke(
            //    ()=>this.DoSomethingLong("buttonParallel_Click_0)"),
            //    () => this.DoSomethingLong("buttonParallel_Click_1)"),
            //    () => this.DoSomethingLong("buttonParallel_Click_2)"),
            //    () => this.DoSomethingLong("buttonParallel_Click_3)"),
            //    () => this.DoSomethingLong("buttonParallel_Click_4)")
            //    );//主线程也来工作了
            //Parallel.For(0, 5, t => DoSomethingLong($"buttonParallel_Click_{t})"));
            //new Action(() =>
            //{
            //    ParallelOptions option = new ParallelOptions()
            //    {
            //        MaxDegreeOfParallelism = 3//最大并行数
            //    };
            //    Parallel.ForEach(new int[] { 1, 2, 3, 4, 5, 6 }, option, t => DoSomethingLong($"buttonParallel_Click_{t}"));
            //}).BeginInvoke(null, null);//这样简单封装一下可以实现主线程不卡

            ParallelOptions option = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 3//最大并行数
            };
            Parallel.ForEach(new int[] { 1, 2, 3, 4, 5, 6 }, option, (t,state) => {
                DoSomethingLong($"buttonParallel_Click_{t}");
                //state.Break();//这一次结束
                //return;
                state.Stop();//整个parallel结束
                return;
            });




            watch.Stop();
            Console.WriteLine($"****************btnParallel_Click   End {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}  {watch.ElapsedMilliseconds}***************");
        }

        #region btnThreadCore_Click
        private static object btnThreadCore_Click_Lock = new object();
        private int TotalCount = 0;//
        private List<int> IntList = new List<int>();


        /// <summary>
        /// 线程安全的集合
        /// </summary>
        System.Collections.Concurrent.ConcurrentDictionary
        /// <summary>
        /// 异常处理、线程取消、多线程的临时变量和线程安全lock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonThreadCore_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine();
            Console.WriteLine($"***********************btnThreadCore_Click Start 主线程id {Thread.CurrentThread.ManagedThreadId}**********************************");
            try
            {
                TaskFactory taskFactory = new TaskFactory();
                List<Task> taskList = new List<Task>();
                #region 异常处理
                //        //在线程Action加上try catch，日志记录，不抛异常
                //        for (int i = 0; i < 20; i++)
                //        {
                //            string name = string.Format($"btnThreadCore_Click_{i}");
                //            Action<object> act = t =>
                //            {
                //                try
                //                {
                //                    Thread.Sleep(2000);
                //                    if (t.ToString().Equals("btnThreadCore_Click_11"))
                //                    {
                //                        throw new Exception(string.Format($"{t} 执行失败"));
                //                    }
                //                    if (t.ToString().Equals("btnThreadCore_Click_12"))
                //                    {
                //                        throw new Exception(string.Format($"{t} 执行失败"));
                //                    }
                //                    Console.WriteLine("{0} 执行成功", t);
                //            }
                //                catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }
                //};
                //            taskList.Add(taskFactory.StartNew(act, name));
                //        }
                //        //异常被吞掉了，加上waitall才能抓取到异常
                //        Task.WaitAll(taskList.ToArray());
                #endregion

                #region 线程取消
                //////线程间都是通过共有变量：都能访问局部变量/全局变量/数据库的一个值/硬盘文件
                //////1,线程不能被外部停止，只能自身停止自身；2,或者在任务启动前停止，会抛出异常的
                //CancellationTokenSource cts = new CancellationTokenSource();
                //for (int i = 0; i < 40; i++)
                //{
                //    string name = string.Format("btnThreadCore_Click{0}", i);
                //    Action<object> act = t =>
                //    {
                //        try
                //        {
                //            //if (cts.IsCancellationRequested)
                //            //{
                //            //    Console.WriteLine("{0} 取消一个任务的执行", t);
                //            //}
                //            Thread.Sleep(2000);
                //            if (t.ToString().Equals("btnThreadCore_Click11"))
                //            {
                //                throw new Exception(string.Format("{0} 执行失败", t));
                //            }
                //            if (t.ToString().Equals("btnThreadCore_Click12"))
                //            {
                //                throw new Exception(string.Format("{0} 执行失败", t));
                //            }
                //            if (cts.IsCancellationRequested)//检查信号量
                //            {
                //                Console.WriteLine("{0} 放弃执行", t);
                //            }
                //            else
                //            {
                //                Console.WriteLine("{0} 执行成功", t);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            cts.Cancel();//表示修改了信号量  让大家取消执行
                //            Console.WriteLine(ex.Message);
                //        }
                //    };
                //    taskList.Add(taskFactory.StartNew(act, name, cts.Token));//没有启动的任务  在Cancel后放弃启动
                //}
                //Task.WaitAll(taskList.ToArray());
                #endregion

                #region 多线程临时变量
                //for (int i = 0; i < 5; i++)
                //{
                //    int k = i;//循环5次实际上是声明了6个临时变量，让我想起了闭包
                //    new Action(() =>
                //    {
                //        Thread.Sleep(100);
                //        //Console.WriteLine(i);
                //        Console.WriteLine(k);
                //    }).BeginInvoke(null, null);
                //}
                #endregion

                #region 线程安全 lock
                ///思考题为什么会有的集合是线程安全的
                //共有变量：都能访问局部变量/全局变量/数据库的一个值/硬盘文件
                for (int i = 0; i < 10000; i++)
                {
                    int newI = i;
                    taskList.Add(taskFactory.StartNew(() =>
                    {
                        lock (btnThreadCore_Click_Lock)//lock后的方法块，任意时刻只有一个线程可以进入
                        {//这里就是单线程，就有悖多线程的处理，影响性能，所以要根据情况添加
                            this.TotalCount += 1;
                            IntList.Add(newI);
                        }
                    }));
                }
                Task.WaitAll(taskList.ToArray());

                Console.WriteLine(this.TotalCount);
                Console.WriteLine(IntList.Count());
                #endregion
            }
            catch (AggregateException aex)
            {
                foreach (var item in aex.InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            watch.Stop();
            Console.WriteLine("**********************btnThreadCore_Click   End 主线程id {0} {1}************************************", Thread.CurrentThread.ManagedThreadId, watch.ElapsedMilliseconds);
            Console.WriteLine();

        }
#endregion
    }
}
