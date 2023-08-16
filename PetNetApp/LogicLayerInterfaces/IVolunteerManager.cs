/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/24
/// 
/// 
/// Interface for VolunteerManager
/// </summary>
/// 
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/24
/// 
/// Final QA
/// </remarks>
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
	public interface IVolunteerManager
	{
		/// <summary>
		/// Oleksiy Fedchuk
		/// Created: 2023/02/24
		/// 
		/// Retrieves Volunteers by fundrasing event Id
		/// </summary>
		/// <remarks>
		/// Zaid Rachman
		/// Updated: 2023/04/24
		/// 
		/// Final QA
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		List<VolunteerVM> RetrieveVolunteersbyFundraisingEventId(int fundraisingEventId);
		/// <summary>
		/// Oleksiy Fedchuk
		/// Create: 2023/04/20
		/// 
		/// </summary>
		/// <remarks>
		/// Zaid Rachman
		/// Updated: 2023/04/24
		/// 
		/// Final QA
		/// </remarks>
		/// <returns></returns>
		List<int> RetrieveAllVolunteers();
		/// <summary>
		/// Oleksiy Fedchuk
		/// Create: 2023/04/20
		/// 
		/// </summary>
		/// <remarks>
		/// Zaid Rachman
		/// Updated: 2023/04/24
		/// 
		/// Final QA
		/// </remarks>
		/// <param name="usersId"></param>
		/// <param name="fundraisingEventId"></param>
		/// <returns></returns>
		bool AddVolunteerToEventbyVolunteerAndEventId(int usersId, int fundraisingEventId);
		/// <summary>
		/// Oleksiy Fedchuk
		/// Create: 2023/04/20
		/// 
		/// </summary>
		/// <remarks>
		/// Zaid Rachman
		/// Updated: 2023/04/24
		/// 
		/// Final QA
		/// </remarks>
		/// <param name="usersId"></param>
		/// <param name="fundraisingEventId"></param>
		/// <returns></returns>
		bool RemoveVolunteerFromEventbyUsersIdAndFundraisingEventId(int usersId, int fundraisingEventId);
	}
}
