#region Imports

using System;
using System.Data;
using System.Data.Common;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data;
using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class DataTransactionTests : UnitTest
    {
        #region Tests

        #region Ctor
        
        [TestMethod]
        public void Ctor_WithValidTransaction ()
        {
            var conn = new TestDbConnection();
            var expectedLevel = IsolationLevel.Snapshot;

            //Act
            var target = new DataTransaction(conn.BeginTransaction(expectedLevel));

            //Assert
            target.IsolationLevel.Should().Be(expectedLevel);            
        }

        [TestMethod]
        public void Ctor_WithNull ()
        {
            Action action = () => new DataTransaction(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_DefaultClosesConnection ()
        {
            //Act
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());
            target.Commit();

            //Assert
            conn.State.Should().Be(ConnectionState.Closed);
        }
        #endregion
        
        #region Commit

        [TestMethod]
        public void Commit_RaisesEvent ()
        {         
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());
            bool wasCommitted = false, wasRolledBack = false;
            
            //Act
            target.Committed += ( o, e ) => wasCommitted = true;
            target.RolledBack += ( o, e ) => wasRolledBack = true;
            target.Commit();

            //Assert
            wasCommitted.Should().BeTrue();
            wasRolledBack.Should().BeFalse();
        }

        [TestMethod]
        public void Commit_ClosesConnection ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction(), true);

            //Act
            target.Commit();

            //Assert
            conn.State.Should().Be(ConnectionState.Closed);
        }

        [TestMethod]
        public void Commit_DoesNotClosesConnection ()
        {
            var conn = new TestDbConnection();            
            var target = new DataTransaction(conn.BeginTransaction(), false);

            //Act
            target.Commit();

            //Assert
            conn.State.Should().Be(ConnectionState.Open);
        }

        [TestMethod]
        public void Commit_MultipleCallsFail ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());

            target.Commit();
            Action action = () => target.Commit();

            action.ShouldThrow<InvalidOperationException>();
        }
        #endregion

        #region Dispose

        [TestMethod]
        public void Dispose_RaisesRolledbackEvent ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());
            bool wasCommitted = false, wasRolledBack = false;

            //Act
            target.Committed += ( o, e ) => wasCommitted = true;
            target.RolledBack += ( o, e ) => wasRolledBack = true;
            target.Dispose();

            //Assert
            wasRolledBack.Should().BeTrue();
            wasCommitted.Should().BeFalse();   
        }

        [TestMethod]
        public void Dispose_MultipleCallsDoNothing ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());

            //Act
            target.Dispose();
            target.Dispose();
        }

        [TestMethod]
        public void Dispose_ClosesConnection ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction(), true);

            //Act
            target.Dispose();

            //Assert
            conn.State.Should().Be(ConnectionState.Closed);
        }

        [TestMethod]
        public void Dispose_DoesNotCloseConnection ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction(), false);

            //Act
            target.Dispose();

            //Assert
            conn.State.Should().Be(ConnectionState.Open);
        }

        [TestMethod]
        public void Dispose_IsolationLevelReset ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());

            //Act
            target.Dispose();

            //Assert
            target.IsolationLevel.Should().Be(IsolationLevel.Unspecified);
        }
        #endregion

        #region Rollback

        [TestMethod]
        public void RollBack_RaisesEvent ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());
            bool wasCommitted = false, wasRolledBack = false;

            //Act
            target.Committed += ( o, e ) => wasCommitted = true;
            target.RolledBack += ( o, e ) => wasRolledBack = true;
            target.Rollback();

            //Assert
            wasRolledBack.Should().BeTrue();
            wasCommitted.Should().BeFalse();            
        }
                
        [TestMethod]
        public void Rollback_ClosesConnection ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction(), true);

            //Act
            target.Rollback();

            //Assert
            conn.State.Should().Be(ConnectionState.Closed);
        }

        [TestMethod]
        public void Rollback_DoesNotClosesConnection ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction(), false);

            //Act
            target.Rollback();

            //Assert
            conn.State.Should().Be(ConnectionState.Open);
        }

        [TestMethod]
        public void Rollback_MultipleCallsFail ()
        {
            var conn = new TestDbConnection();
            var target = new DataTransaction(conn.BeginTransaction());

            target.Rollback();

            Action action = () => target.Rollback();

            action.ShouldThrow<InvalidOperationException>();
        }
        #endregion

        #endregion

        #region Private Members

        private class TestDbConnection : DbConnection
        {
            protected override DbTransaction BeginDbTransaction ( IsolationLevel isolationLevel )
            {
                if (State != ConnectionState.Open)
                    m_state = ConnectionState.Open;

                return new TestDbTransaction(this, isolationLevel);
            }

            public override void ChangeDatabase ( string databaseName )
            {                
            }

            public override void Close ()
            {
                m_state = ConnectionState.Closed;             
            }

            public override string ConnectionString { get; set; }

            protected override DbCommand CreateDbCommand ()
            {
                return null;
            }

            public override string DataSource
            {
                get { return ""; }
            }

            public override string Database
            {
                get { return ""; }
            }

            public override void Open ()
            {
                if (m_state != ConnectionState.Closed)
                    throw new InvalidOperationException("Not closed.");

                m_state = ConnectionState.Open;
            }

            public override string ServerVersion
            {
                get { return ""; }
            }

            public override ConnectionState State
            {
                get { return m_state; }
            }

            private ConnectionState m_state = ConnectionState.Closed;
        }

        private class TestDbTransaction : DbTransaction
        {
            public TestDbTransaction ( DbConnection connection, IsolationLevel level )
            {
                m_conn = connection;
                m_level = level;
            }

            public override void Commit ()
            {
                WasCommited = true;
            }

            protected override DbConnection DbConnection
            {
                get { return m_conn; }
            }

            public override IsolationLevel IsolationLevel
            {
                get { return m_level; }
            }

            public override void Rollback ()
            {
                WasRolledBack = true;
            }

            public bool WasCommited { get; private set; }
            public bool WasRolledBack { get; private set; }

            private IsolationLevel m_level;
            private DbConnection m_conn;
        }
        #endregion
    }
}
