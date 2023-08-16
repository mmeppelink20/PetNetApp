using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTest
{
    [TestClass]
    public class ReplyManagerTests
    {
        ReplyManager replyManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            replyManager = new ReplyManager(new DataAccessLayerFakes.ReplyAccessorFake());
        }

        [TestMethod]
        public void TestRetrieveAllReplies()
        {
            int expectedResult = 2;
            int actualResult = 0;
            int postId = 1;

            actualResult = replyManager.RetrieveAllRepliesByPostId(postId).Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveActiveReplies()
        {
            int expectedResult = 1;
            int actualResult = 0;
            int postId = 2;

            actualResult = replyManager.RetrieveActiveRepliesByPostId(postId).Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCountActiveReplies()
        {
            int expectedResult = 2;
            int actualResult = 0;
            int postId = 1;

            actualResult = replyManager.RetrieveCountActiveRepliesByPostId(postId);
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestCountReplies()
        {
            int expectedResult = 1;
            int actualResult = 0;
            int postId = 2;

            actualResult = replyManager.RetrieveCountRepliesByPostId(postId);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAddPost()
        {
            int expectedResult = 1;
            int actualResult = 0;
            ReplyVM reply = new ReplyVM();
            reply.ReplyId = 4;

            actualResult = replyManager.AddReply(reply);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEditReply()
        {
            bool expectedResult = true;
            bool actualResult = false;
            ReplyVM reply = new ReplyVM();
            reply.ReplyId = 3;

            ReplyVM newReply = new ReplyVM();
            newReply.ReplyContent = "Test";
            newReply.ReplyDate = DateTime.Today;

            actualResult = replyManager.EditReply(reply, newReply);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveReplyByReplyId()
        {
            int expectedId = 1;
            int actualId = 0;

            actualId = replyManager.RetrieveReplyByReplyId(1).ReplyId;
            Assert.AreEqual(expectedId, actualId);
        }

        /// <summary>
		/// Andrew Cromwell
		/// Created: 2023/04/14
		/// </summary>
        [TestMethod]
        public void TestEditReplyVisibilityByReplyIdReturnsTrueWhenTheReplyIsUpdated()
        {
            ReplyVM replyToEdit = new ReplyVM()
            {
                ReplyId = 1,
                PostId = 1,
                ReplyAuthor = 1,
                ReplyContent = "Post Contents",
                ReplyDate = DateTime.Today,
                ReplyVisibility = true,
                ReplierGivenName = "Gwen",
                ReplierFamilyName = "Arman",
                UserReplyReport = false
            };
            replyToEdit.ReplyVisibility = false;

            bool result = replyManager.EditReplyVisibilityByReplyId(replyToEdit);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAddPostReport()
        {
            bool result = false;

            result = replyManager.AddReplyReport(1, 1, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestRemovePostReport()
        {
            bool result = false;

            result = replyManager.RemoveReplyReport(1, 1);

            Assert.IsTrue(result);
        }

    }
}
