using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class ValidationiHelpersTests
    {
        [TestMethod]
        public void TestBadEmails()
        {
            string badEmail1 = "@.";
            string badEmail2 = "a@a.a";
            string badEmail3 = "abc@ab$c.com";
            string badEmail4 = "abc#@td.td";
            string badEmail5 = "abc-@ab.ab";
            string badEmail6 = "abc..abc@abc.abc";
            string badEmail7 = "abc--abc@abc.abc";
            string badEmail8 = "abc-abc.abc.abc";
            string badEmail9 = "abc_abc@abc@abc";
            string badEmail10 = "abc.abc@abc";
            string badEmail11 = "abc.abc@abc..com";
            string badEmail12 = "abc.abc@abc-com";
            string badEmail13 = "abc.abc@abc!com";
            string badEmail14 = "abc.abc@.abccom";
            string badEmail15 = "abc@abc@asdf.abccom";
            string badEmail16 = "abcc.@asdf.abccom";
            string badEmail17 = "abc-.abc@abc.abc";
            string badEmail18 = "abc-_abc@abc.abc";
            string badEmail19 = "abc_.abc@abc.abc";
            string badEmail20 = "abc__abc@abc.abc";
            string badEmail21 = ".abc_abc@abc.abc";
            string badEmail22 = "abcdefghijkabcdefghijkabcdefghijkabcdefghijkabcdefghijkabcdefghijkabcdefghijk@abc.abc";
            string badEmail23 = "abcdefghijkabcdefghijkabcdefghijkabcdefghijcdefghijkabcdefghijk4@abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz.abc";
            string badEmail24 = "abc_abc@--c.abc";
            string badEmail25 = "normalemai@with space.com";
            string badEmail26 = "normalemai@with*symbol.com";


            Assert.IsFalse(badEmail1.IsValidEmail()); 
            Assert.IsFalse(badEmail2.IsValidEmail()); 
            Assert.IsFalse(badEmail3.IsValidEmail()); 
            Assert.IsFalse(badEmail4.IsValidEmail()); 
            Assert.IsFalse(badEmail5.IsValidEmail()); 
            Assert.IsFalse(badEmail6.IsValidEmail()); 
            Assert.IsFalse(badEmail7.IsValidEmail()); 
            Assert.IsFalse(badEmail8.IsValidEmail()); 
            Assert.IsFalse(badEmail9.IsValidEmail());
            Assert.IsFalse(badEmail10.IsValidEmail());
            Assert.IsFalse(badEmail11.IsValidEmail());
            Assert.IsFalse(badEmail12.IsValidEmail());
            Assert.IsFalse(badEmail13.IsValidEmail());
            Assert.IsFalse(badEmail14.IsValidEmail());
            Assert.IsFalse(badEmail15.IsValidEmail());
            Assert.IsFalse(badEmail16.IsValidEmail());
            Assert.IsFalse(badEmail17.IsValidEmail());
            Assert.IsFalse(badEmail18.IsValidEmail());
            Assert.IsFalse(badEmail19.IsValidEmail());
            Assert.IsFalse(badEmail20.IsValidEmail());
            Assert.IsFalse(badEmail21.IsValidEmail());
            Assert.IsFalse(badEmail22.IsValidEmail());
            Assert.IsFalse(badEmail23.IsValidEmail());
            Assert.IsFalse(badEmail24.IsValidEmail());
            Assert.IsFalse(badEmail25.IsValidEmail());
            Assert.IsFalse(badEmail26.IsValidEmail());
        }

        [TestMethod]
        public void TestGoodEmails()
        {
            string goodEmail1 = "abc.abc.abc@abc.abc-abc";
            string goodEmail2 = "abcdefghijkabcdefghijkabcdefghijkabcdefgdhijcdefgijkabcdefghijk4@abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxywxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz.abc";
            string goodEmail3 = "b@a.ab";
            string goodEmail4 = "abc.abc_abc@abc.abc.abc";
            string goodEmail5 = "abc_abcabc@abc.abc.abc";
            string goodEmail6 = "abc@abc.abc";
            string goodEmail7 = "b@ac.ab";
            string goodEmail8 = "a0_12@abc.abc";
            string goodEmail9 = "01-a0@abc.abc";
            string goodEmail10 = "abc.abc@amazon.co.uk.international";
            string goodEmail11 = "k0123456@student.kirkwood.edu";
            string goodEmail12 = "stephen-jaurigue@student.kirkwood.edu";

            Assert.IsTrue(goodEmail1.IsValidEmail());
            Assert.IsTrue(goodEmail2.IsValidEmail());
            Assert.IsTrue(goodEmail3.IsValidEmail());
            Assert.IsTrue(goodEmail4.IsValidEmail());
            Assert.IsTrue(goodEmail5.IsValidEmail());
            Assert.IsTrue(goodEmail6.IsValidEmail());
            Assert.IsTrue(goodEmail7.IsValidEmail());
            Assert.IsTrue(goodEmail8.IsValidEmail());
            Assert.IsTrue(goodEmail9.IsValidEmail());
            Assert.IsTrue(goodEmail10.IsValidEmail());
            Assert.IsTrue(goodEmail11.IsValidEmail());
            Assert.IsTrue(goodEmail12.IsValidEmail());
        }
    
        [TestMethod]
        public void TestBadAmounts()
        {
            string badAmount1 = "00";
            string badAmount2 = "123456.12";
            string badAmount3 = "12345.";
            string badAmount4 = "123.123";
            string badAmount5 = ".12";
            string badAmount6 = "ab.ab";
            string badAmount7 = "80.a";
            string badAmount8 = "a.21";
            string badAmount9 = "123456";
            string badAmount10 = "001.12";

            Assert.IsFalse(badAmount1.IsValidAmount());
            Assert.IsFalse(badAmount2.IsValidAmount());
            Assert.IsFalse(badAmount3.IsValidAmount());
            Assert.IsFalse(badAmount4.IsValidAmount());
            Assert.IsFalse(badAmount5.IsValidAmount());
            Assert.IsFalse(badAmount6.IsValidAmount());
            Assert.IsFalse(badAmount7.IsValidAmount());
            Assert.IsFalse(badAmount8.IsValidAmount());
            Assert.IsFalse(badAmount9.IsValidAmount());
            Assert.IsFalse(badAmount10.IsValidAmount());
        }

        [TestMethod]
        public void TestGoodAmounts()
        {
            string goodAmount1 = "12.1";
            string goodAmount2 = "0.0";
            string goodAmount3 = "0.12";
            string goodAmount4 = "12345.12";
            string goodAmount5 = "12.00";
            string goodAmount6 = "9.99";
            string goodAmount7 = "0";
            string goodAmount8 = "9";
            string goodAmount9 = "12345";
            string goodAmount10 = "90000";

            Assert.IsTrue(goodAmount1.IsValidAmount());
            Assert.IsTrue(goodAmount2.IsValidAmount());
            Assert.IsTrue(goodAmount3.IsValidAmount());
            Assert.IsTrue(goodAmount4.IsValidAmount());
            Assert.IsTrue(goodAmount5.IsValidAmount());
            Assert.IsTrue(goodAmount6.IsValidAmount());
            Assert.IsTrue(goodAmount7.IsValidAmount());
            Assert.IsTrue(goodAmount8.IsValidAmount());
            Assert.IsTrue(goodAmount9.IsValidAmount());
            Assert.IsTrue(goodAmount10.IsValidAmount());
        }

        [TestMethod]
        public void TestBadZipcodes()
        {
            string badZip1 = "123456";
            string badZip2 = "1234";
            string badZip3 = "abcde";
            string badZip4 = "abcdefghj";
            string badZip5 = "1234567890";
            string badZip6 = "12345 123";
            string badZip7 = "12345 1234";
            string badZip8 = "     ";
            string badZip9 = "";
            string badZip10 = null;

            Assert.IsFalse(badZip1.IsValidZipcode());
            Assert.IsFalse(badZip2.IsValidZipcode());
            Assert.IsFalse(badZip3.IsValidZipcode());
            Assert.IsFalse(badZip4.IsValidZipcode());
            Assert.IsFalse(badZip5.IsValidZipcode());
            Assert.IsFalse(badZip6.IsValidZipcode());
            Assert.IsFalse(badZip7.IsValidZipcode());
            Assert.IsFalse(badZip8.IsValidZipcode());
            Assert.IsFalse(badZip9.IsValidZipcode());
            Assert.IsFalse(badZip10.IsValidZipcode());
        }
        [TestMethod]
        public void TestGoodZipcodes()
        {
            string goodZip1 = "34567";
            string badZip1 = "123456789";
            string goodZip3 = "96555";

            Assert.IsTrue(goodZip1.IsValidZipcode());
            Assert.IsTrue(!badZip1.IsValidZipcode());
            Assert.IsTrue(goodZip3.IsValidZipcode());
        }
        
        [TestMethod]
        public void TestBadShortDescription()
        {
            string badShortDescription1 = "fgvneggvdiztlrbclfdboykqatoxqdxkajwhpbqyvlfneinbdmbwgdkyzmqhbhzwkmlsvbsdxhtwvvepkndnvkmmbgrcutblncnpwzvtykzsiedgrgkrmqwlwqgjojsbrqgfvtkrxtiptvrxuiwyopwaawnsvzmmxtzdukxjwyutathsdvmhvgpqjxhrwybvlvcvbodpoamvcnzlpdzcwxyuakstpanqzrwkyhpwpzbxzdqmytktlmnzzjqzzkvvecsqyjkqkofdmnperfwryodvzqrtsmfboxvuwuhnewqmptcpkenkbfg";
            Assert.IsFalse(badShortDescription1.IsValidShortDescription());
        }

        [TestMethod]
        public void TestGoodShortDescription()
        {
            string goodShortDescription1 = "cbfunsetpxrewoaforrvdvkqhekyuvogxmxgmpuzaijkkypcyptjlqrjzcszuiwxmjzyqgpzktftzhpldydvtnhjogihehtjowodmmgmyoemvfawrfjkcophfijyaymrsxaoquabjsqfacsvvdsfwkuwgfpvjphgdykukzowyouvzungbscrkxgbeoldkbczesokbdpgnqbwmosdzfverfcqdjkwycdzgiblqzpurbfkbpdkftrcpgozms";
            string goodShortDescription2 = "";
            string goodShortDescription3 = null;
            string goodShortDescription4 = "Normal Length with 12933851451!@#$6";

            Assert.IsTrue(goodShortDescription1.IsValidShortDescription());
            Assert.IsTrue(goodShortDescription2.IsValidShortDescription());
            Assert.IsTrue(goodShortDescription3.IsValidShortDescription());
            Assert.IsTrue(goodShortDescription4.IsValidShortDescription());
        }
        
        [TestMethod]
        public void TestBadLongDescription()
        {
            string badLongDescription1 = "fgvneggvdiztlrbclfdboykqatoxqdxkajwhpbqyvlfneinbdmbwgdkyzmqhbhzwkmlsvbsdxhtwvvepkndnvkmmbgrcutblncnpwzvtykzsiedgrgkrmqwlwqgjojsbrqgfvtkrxtiptvrxuiwyopwaawnsvzmmxtzdukxjwyutathsdvmhvgpqjxhrwybvlvcvbodpoamvcnzlpdzcwxyuakstpanqzrwkyhpwpzbxzdqmytktlmnzzjqzzkvvecsqyjkqkofdmnperfwryodvzqrtsmfboxvuwuhnewqmptcpkenkbfgfgvneggvdiztlrbclfdboykqatoxqdxkajwhpbqyvlfneinbdmbwgdkyzmqhbhzwkmlsvbsdxhtwvvepkndnvkmmbgrcutblncnpwzvtykzsiedgrgkrmqwlwqgjojsbrqgfvtkrxtiptvrxuiwyopwaawnsvzmmxtzdukxjwyutathsdvmhvgpqjxhrwy";

            Assert.IsFalse(badLongDescription1.IsValidLongDescription());
        }

        [TestMethod]
        public void TestGoodLongDescription()
        {
            string goodLongDescription1 = "fgvneggvdiztlrbclfdboykqatoxqdxkajwhpbqyvlfneinbdmbwgdkyzmqhbhzwkmlsvbsdxhtwvvepkndnvkmmbgrcutblncnpwzvtykzsiedgrgkrmqwlwqgjojsbrqgfvtkrxtiptvrxuiwyopwaawnsvzmmxtzdukxjwyutathsdvmhvgpqjxhrwybvlvcvbodpoamvcnzlpdzcwxyuakstpanqzrwkyhpwpzbxzdqmytktlmnzzjqzzkvvecsqyjkqkofdmnperfwryodvzqrtsmfboxvuwuhnewqmptcpkenkbfgfgvneggvdiztlrbclfdboykqatoxqdxkajwhpbqyvlfneinbdmbwgdkyzmqhbhzwkmlsvbsdxhtwvvepkndnvkmmbgrcutblncnpwzvtykzsiedgrgkrmqwlwqgjojsbrqgfvtkrxtiptvrxuiwyopwaawnsvzmmxtzdukxjwyutathsdvmhvgpqjxhrw";
            string goodLongDescription2 = "";
            string goodLongDescription3 = null;
            string goodLongDescription4 = "Normal Length with 12933851451!@#$6";

            Assert.IsTrue(goodLongDescription1.IsValidLongDescription());
            Assert.IsTrue(goodLongDescription2.IsValidLongDescription());
            Assert.IsTrue(goodLongDescription3.IsValidLongDescription());
            Assert.IsTrue(goodLongDescription4.IsValidLongDescription());
        }
    
        [TestMethod]
        public void TestBadAddresses()
        {
            string badAddress1 = "aaaaaaaaaaaassssssssssssdddddddddffffffgggggggggghi";
            string badAddress2 = "";
            string badAddress3 = null;

            Assert.IsFalse(badAddress1.IsValidAddress());
            Assert.IsFalse(badAddress2.IsValidAddress());
            Assert.IsFalse(badAddress3.IsValidAddress());
        }

        [TestMethod]
        public void TestGoodAddresses()
        {
            string goodAddress1 = "a";
            string goodAddress2 = "aaaaaaaaaaaassssssssssssdddddddddffffffggggggggggh";
            string goodAddress3 = "134 awesomesauce st";

            Assert.IsTrue(goodAddress1.IsValidAddress());
            Assert.IsTrue(goodAddress2.IsValidAddress());
            Assert.IsTrue(goodAddress3.IsValidAddress());
        }
        [TestMethod]
        public void TestBadAddresses2()
        {
            string badAddress1 = "aaaaaaaaaaaassssssssssssdddddddddffffffgggggggggghi";
            Assert.IsFalse(badAddress1.IsValidAddress2());
        }

        [TestMethod]
        public void TestGoodAddresses2()
        {
            string goodAddress4 = "";
            string goodAddress5 = null;
            string goodAddress1 = "a";
            string goodAddress2 = "aaaaaaaaaaaassssssssssssdddddddddffffffggggggggggh";
            string goodAddress3 = "134 awesomesauce st";

            Assert.IsTrue(goodAddress1.IsValidAddress2());
            Assert.IsTrue(goodAddress2.IsValidAddress2());
            Assert.IsTrue(goodAddress3.IsValidAddress2());
            Assert.IsTrue(goodAddress4.IsValidAddress2());
            Assert.IsTrue(goodAddress5.IsValidAddress2());
        }

        [TestMethod]
        public void TestBadPhoneNumbers()
        {
            string badPhone1 = "abcuanmemd";
            string badPhone2 = "12345678901234";
            string badPhone3 = "123-456-7890";
            string badPhone4 = "123 456 7890";
            string badPhone5 = "abcdefhijk";
            string badPhone6 = "123456789";

            Assert.IsFalse(badPhone1.IsValidPhone());
            Assert.IsFalse(badPhone2.IsValidPhone());
            Assert.IsFalse(badPhone3.IsValidPhone());
            Assert.IsFalse(badPhone4.IsValidPhone());
            Assert.IsFalse(badPhone5.IsValidPhone());
            Assert.IsFalse(badPhone6.IsValidPhone());
        }

        [TestMethod]
        public void TestGoodPhoneNumbers()
        {
            string goodPhone1 = "1234567890";
            string goodPhone2 = "12345678910";
            string goodPhone3 = "1234567890123";

            Assert.IsTrue(goodPhone1.IsValidPhone());
            Assert.IsTrue(goodPhone2.IsValidPhone());
            Assert.IsTrue(goodPhone3.IsValidPhone());
        }
        [TestMethod]
        public void TestBadLastNames()
        {
            string badName1 = "aaaaaaaaaaaassssssssssssdddddddddffffffgggggggggghi";
            string badName2 = "";
            string badName3 = null;
            string badName4 = "2";
            string badName5 = "a2";
            string badName6 = "a2e";
            string badName7 = "-ae";
            string badName8 = ".ae";
            string badName9 = "0ab";
            string badName10 = "AB@";
            string badName11 = " bad";

            Assert.IsFalse(badName1.IsValidLastName());
            Assert.IsFalse(badName2.IsValidLastName());
            Assert.IsFalse(badName3.IsValidLastName());
            Assert.IsFalse(badName4.IsValidLastName());
            Assert.IsFalse(badName5.IsValidLastName());
            Assert.IsFalse(badName6.IsValidLastName());
            Assert.IsFalse(badName7.IsValidLastName());
            Assert.IsFalse(badName8.IsValidLastName());
            Assert.IsFalse(badName9.IsValidLastName());
            Assert.IsFalse(badName10.IsValidLastName());
            Assert.IsFalse(badName11.IsValidLastName());
        }

        [TestMethod]
        public void TestGoodLastNames()
        {
            string goodName1 = "a";
            string goodName2 = "alakazam";
            string goodName3 = "bippitybopity";
            string goodName4 = "abra kadabra";
            string goodName5 = "skippedty-bop";
            string goodName6 = "Ab3";
            string goodName7 = "a-brian";
            string goodName8 = "O'Riely";
            string goodName9 = "aok--bop";
            string goodName10 = "aaaaaaaaaaaassssssssssssdddddddddffffffggggggggggh";

            Assert.IsTrue(goodName1.IsValidLastName());
            Assert.IsTrue(goodName2.IsValidLastName());
            Assert.IsTrue(goodName3.IsValidLastName());
            Assert.IsTrue(goodName4.IsValidLastName());
            Assert.IsTrue(goodName5.IsValidLastName());
            Assert.IsTrue(goodName6.IsValidLastName());
            Assert.IsTrue(goodName7.IsValidLastName());
            Assert.IsTrue(goodName8.IsValidLastName());
            Assert.IsTrue(goodName9.IsValidLastName());
            Assert.IsTrue(goodName10.IsValidLastName());
        }

        [TestMethod]
        public void TestBadFirstNames()
        {
            string badName1 = "aaaaaaaaaaaassssssssssssdddddddddffffffgggggggggghi";
            string badName2 = "";
            string badName3 = null;
            string badName4 = "2";
            string badName7 = "-ae";
            string badName8 = ".ae";
            string badName9 = "0ab";
            string badName10 = "AB@";
            string badName11 = " bad";

            Assert.IsFalse(badName1.IsValidFirstName());
            Assert.IsFalse(badName2.IsValidFirstName());
            Assert.IsFalse(badName3.IsValidFirstName());
            Assert.IsFalse(badName4.IsValidFirstName());
            Assert.IsFalse(badName7.IsValidFirstName());
            Assert.IsFalse(badName8.IsValidFirstName());
            Assert.IsFalse(badName9.IsValidFirstName());
            Assert.IsFalse(badName10.IsValidFirstName());
            Assert.IsFalse(badName11.IsValidFirstName());
        }

        [TestMethod]
        public void TestGoodFirstNames()
        {
            string goodName1 = "a";
            string goodName2 = "alakazam";
            string goodName3 = "bippitybopity";
            string goodName4 = "abra kadabra";
            string goodName5 = "skippedty-bop";
            string goodName6 = "Ab3";
            string goodName7 = "a-brian";
            string goodName8 = "O'Riely";
            string goodName9 = "aok--bop";
            string goodName10 = "aaaaaaaaaaaassssssssssssdddddddddffffffggggggggggh";
            string goodName11 = "A2e";
            string goodName12 = "a0'ben";

            Assert.IsTrue(goodName1.IsValidFirstName());
            Assert.IsTrue(goodName2.IsValidFirstName());
            Assert.IsTrue(goodName3.IsValidFirstName());
            Assert.IsTrue(goodName4.IsValidFirstName());
            Assert.IsTrue(goodName5.IsValidFirstName());
            Assert.IsTrue(goodName6.IsValidFirstName());
            Assert.IsTrue(goodName7.IsValidFirstName());
            Assert.IsTrue(goodName8.IsValidFirstName());
            Assert.IsTrue(goodName9.IsValidFirstName());
            Assert.IsTrue(goodName10.IsValidFirstName());
            Assert.IsTrue(goodName11.IsValidFirstName());
            Assert.IsTrue(goodName12.IsValidFirstName());
        }
    }
}
