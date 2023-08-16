using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    /// <summary>
    /// Author: Gwen Arman
    /// Date: 2023/03/02
    /// Description: Selects all of the donations by ShelterId
    /// </summary>
    /// <param name="ShelterId"></param>
    /// <returns></returns>
    public interface IDonationAccessor
    {
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023/03/01
        /// Description: Selects donations by a shelter id
        /// </summary>
        /// <param name="ShelterId"></param>
        /// <returns></returns>
        List<DonationVM> SelectDonationsByShelterId(int ShelterId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023/03/21
        /// Description: Selects all donations
        /// </summary>
        /// <returns></returns>
        List<DonationVM> SelectAllDonations();
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023/03/08
        /// Description: Selects all inkinds by donation id
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="donationId"></param>
        /// <returns></returns>
        List<InKind> SelectInKindsByDonationId(int donationId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023/03/08
        /// Description: Selects donation by donation id
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="donationID"></param>
        /// <returns></returns>
        DonationVM SelectDonationByDonationId(int donationID);
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/17
        /// Description: Selects all of the donations by eventId
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        List<DonationVM> SelectDonationsByEventId(int eventId);
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/17
        /// Description: Selects sum of all of the donations by eventId
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        decimal SelectSumDonationsByEventId(int eventId);
        /// <summary>
        /// Author: Teft Francisco
        /// Date: 2023/03/14
        /// Description: Retrieves a user's donations by their user id
        /// </summary>
        /// <param name="UsersId"></param>
        /// <returns></returns>
        List<DonationVM> SelectDonationsByUserId(int usersId);

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/13
        /// Description: Inserts a donation record
        /// </summary>
        /// <param name="donation">The donation to insert</param>
        /// <returns>The DonationId of the newly inserted Donation</returns>
        int InsertDonation(Donation donation);

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/13
        /// Description: Inserts an InKind record
        /// </summary>
        /// <param name="inKind">The inkind to insert</param>
        /// <returns>Rows affected</returns>
        int InsertInKind(InKind inKind);
    }
}
