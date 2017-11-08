using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkMessage;
using System.ComponentModel;
using System.Timers;

namespace StudentenAdministratieApp.ViewModel
{
    /// <summary>
    /// Update lijsten van de database
    /// Heeft een action nodig die executeaction en id aanneemt.
    /// </summary>
    public class clsListUpdater
    {
        private static BLL.clsCommonBLL BLL = new BLL.clsCommonBLL();
        private static bool RUNNING = false;

        public static Task UpdateTask;

        public static Timer updateTimer;
        public clsListUpdater()
        {
            //STOP DESIGNERMODE FROM LOADING THE SOCKET!!!!
            if (!DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                UpdateTask = Task.Factory.StartNew(() => NetworkMessage.clsReceiveMessage.ReceiveMessage(HandleNetworkUpdate));
            updateTimer = new Timer(60000);
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            //updateTimer.Enabled = true;
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (Action a in SequentialUpdateList)
            {
                a();
            }
        }

        /// <summary>
        /// The Action executed by the database
        /// </summary>
        public enum ExecuteAction
        {
            UPDATE, DELETE, CREATE
        }

        //update lists for all instances
        /// <summary>
        /// lists not getting added, action is not unique for string id
        /// </summary>
        //private static ConcurrentDictionary<string, Action<ExecuteAction, int>> RegisteredActions = new ConcurrentDictionary<string, Action<ExecuteAction, int>>();

        private static ConcurrentDictionary<string, ConcurrentDictionary<object, Action<ExecuteAction, int>>> RegisteredActions = new ConcurrentDictionary<string, ConcurrentDictionary<object, Action<ExecuteAction, int>>>();

        //Skip updates you yourself will send, not static
        private ConcurrentDictionary<string, Tuple<ExecuteAction, int>> SkipNexUpdate = new ConcurrentDictionary<string, Tuple<ExecuteAction, int>>();

        //timed updates
        public static ConcurrentBag<Action> SequentialUpdateList = new ConcurrentBag<Action>();
        //private static ConcurrentDictionary<IEnumerable, Action<IEnumerable>>

        /// <summary>
        /// Registers the method with type identifier
        /// converts the type to a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="todo"></param>
        public static void RegisterForUpdates<T>(object caller, Action<ExecuteAction, int> todo)
        {
            Type t = typeof(T);
            Console.WriteLine("Registered classes:" + RegisteredActions.Count);
            ConcurrentDictionary<object, Action<ExecuteAction, int>> dict;
            if (!RegisteredActions.TryGetValue(t.Name, out dict))
            {
                dict = new ConcurrentDictionary<object, Action<ExecuteAction, int>>();
                RegisteredActions.TryAdd(t.Name, dict);

            }
            if (dict.TryAdd(caller, todo))
            {
                Console.WriteLine("Method has been added: " + caller.GetType().Name);
            }
            else
            {
                Console.WriteLine("Method has not been added: " + caller.GetType().Name);
            }

        }

        /// <summary>
        /// Remove from registering
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void UnRegister<T>(object caller)
        {
            ConcurrentDictionary<object, Action<ExecuteAction, int>> e;
            if (RegisteredActions.TryGetValue(typeof(T).Name, out e))
            {
                Action<ExecuteAction, int> method;
                if (e.TryRemove(caller, out method))
                {
                    Console.WriteLine("Method has been removed");
                }
                else
                {
                    Console.WriteLine("Method has not been removed");
                }
            }
            else
            {
                Console.WriteLine("Could not find caller:" + caller.GetType().Name);
            }

        }

        /// <summary>
        /// Do Update, but don't receive update.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="ID"></param>
        public void DoUpdate<T>(ExecuteAction ex, int ID)
        {
            SkipNexUpdate.TryAdd(typeof(T).Name, new Tuple<ExecuteAction, int>(ex, ID));
            SendNetworkUpdate<T>(ex, ID);
        }

        public void DoUpdate<T>(ExecuteAction ex, int ID, Action AfterMessageSent)
        {
            SkipNexUpdate.TryAdd(typeof(T).Name, new Tuple<ExecuteAction, int>(ex, ID));
            SendNetworkUpdate<T>(ex, ID, AfterMessageSent);
        }
        /// <summary>
        /// Handles the message received in the broadcast message
        /// Layout of the message:
        /// ExecuteAction:Type:ID
        /// </summary>
        /// <param name="data"></param>
        /// <param name="bytecount"></param>
        public void HandleNetworkUpdate(byte[] data, int bytecount)
        {
            try
            {
                string stringData = Encoding.ASCII.GetString(data, 0, bytecount);
                Console.WriteLine(stringData);

                if (!string.IsNullOrEmpty(stringData))
                {
                    if (stringData.Count(x => x == ':') == 2)
                    {
                        Console.WriteLine("valid message");
                        string[] splitData = stringData.Split(':');
                        Console.WriteLine("get enum");
                        ExecuteAction ac = (ExecuteAction)Enum.Parse(typeof(ExecuteAction), splitData[1]);
                        Console.WriteLine("get objectname");
                        string objectName = splitData[0];
                        Console.WriteLine("get id");
                        int ID = int.Parse(splitData[2]);
                        Console.WriteLine("initialise actions");
                        ConcurrentDictionary<object, Action<ExecuteAction, int>> todo;
                        Tuple<ExecuteAction, int> ob;
                        Console.WriteLine("before dictionary check");
                        //if objectname not in skipupdate, do action else, remove the skipaction.
                        //testing skipnextupdate moet op false staan

                        if (!SkipNexUpdate.TryGetValue(objectName, out ob))
                        {
                            Console.WriteLine("not in skipnextupdate");

                            if (RegisteredActions.TryGetValue(objectName, out todo))
                            {
                                Console.WriteLine("registeredActions");
                                //run action
                                todo.Values.ToList().ForEach(x => x(ac, ID));
                                Console.WriteLine("action done");
                            }
                        }
                        else
                        {
                            Console.WriteLine("before tryremove");
                            SkipNexUpdate.TryRemove(objectName, out ob);
                        }

                    }




                }


            }
            catch (Exception e)
            {
                Console.WriteLine("clsListUpdater:" + e.Message);
            }

        }


        public void SendNetworkUpdate<T>(ExecuteAction ac, int ID)
        {
            Task.Factory.StartNew(() =>
            {
                //SkipNexUpdate.TryAdd(typeof(T).Name, new Tuple<ExecuteAction, int>(ac, ID));
                NetworkMessage.clsSendMessage.SendMessage(typeof(T).Name + ":" + ac.ToString() + ":" + ID);


            });

        }

        public async void SendNetworkUpdate<T>(ExecuteAction ac, int ID, Action AfterMessageSent)
        {
            await Task.Factory.StartNew(() =>
            {
                //SkipNexUpdate.TryAdd(typeof(T).Name, new Tuple<ExecuteAction, int>(ac, ID));
                NetworkMessage.clsSendMessage.SendMessage(typeof(T).Name + ":" + ac.ToString() + ":" + ID);


            });
            AfterMessageSent();
        }


    }
}
