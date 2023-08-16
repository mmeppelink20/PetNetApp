using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTest
{
    [TestClass]
    public class PostManagerTests
    {
        PostManager postManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            postManager = new PostManager(new DataAccessLayerFakes.PostAccessorFake());
        }

        [TestMethod]
        public void TestRetrieveAllPosts()
        {
            int expectedResult = 4;
            int actualResult = 0;

            actualResult = postManager.RetrieveAllPosts().Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveActivePosts()
        {
            int expectedResult = 3;
            int actualResult = 0;

            actualResult = postManager.RetrieveActivePosts().Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAddPost()
        {
            int expectedResult = 1;
            int actualResult = 0;
            PostVM post = new PostVM();
            post.PostId = 4;

            actualResult = postManager.AddPost(post);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEditPost()
        {
            bool expectedResult = true;
            bool actualResult = false;
            PostVM post = new PostVM();
            post.PostId = 5;

            PostVM newPost = new PostVM();
            newPost.PostContent = "Test";
            newPost.PostDate = DateTime.Today;

            actualResult = postManager.EditPost(post, newPost);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrievePostByPostId()
        {
            int expectedId = 1;
            int actualId = 0;

            actualId = postManager.RetrievePostByPostId(1).PostId;
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void TestEditPostVisibility()
        {
            bool expectedResult = true;
            bool actualResult = false;
            PostVM post = new PostVM();
            post.PostId = 5;

            bool newVisibility = true;


            actualResult = postManager.EditPostVisibility(post.PostId, newVisibility, post.PostVisibility);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveReportMessages()
        {
            int expected = 2;
            int actual = 0;

            actual = postManager.RetrieveReportMessages().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddPostReport()
        {
            bool result = false;

            result = postManager.AddPostReport(1, 1, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestRemovePostReport()
        {
            bool result = false;

            result = postManager.RemovePostReport(1, 1);

            Assert.IsTrue(result);
        }
    }
}
