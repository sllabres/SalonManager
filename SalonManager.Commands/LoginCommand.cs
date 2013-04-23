using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore;
using EventStore.Dispatcher;


namespace SalonManager.Commands
{
    public class LoginCommand
    {
        private IStoreEvents _store;

        public LoginCommand()
        {
            var test = new CommitDispatcher();
            _store = Wireup.Init()
                .UsingRavenPersistence("RavenDb")
                .InitializeStorageEngine()
                .UsingSynchronousDispatchScheduler()                
                .DispatchTo(test)
                .Build();            
        }

        public void Execute()
        {
            var id = Guid.NewGuid();

            using (var stream = _store.CreateStream(id))
            {
                stream.Add(new EventMessage { Body = "Hello world!" });
                stream.CommitChanges(id);                
            }
                        
            using (var stream = _store.OpenStream(id, 0, int.MaxValue))
            {
                foreach (var @event in stream.CommittedEvents)
                {
                    // business processing...
                }
            }

            var commits = _store.Advanced.GetFrom(DateTime.Now.AddDays(-1));
        }
    }

    public class CommitDispatcher : IDispatchCommits
    {
        #region IDispatchCommits Members

        public void Dispatch(Commit commit)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}