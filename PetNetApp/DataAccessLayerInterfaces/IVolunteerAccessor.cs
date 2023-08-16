/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/24
/// 
/// 
/// Interface for VolunteerAccessor
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

namespace DataAccessLayerInterfaces
{
	public interface IVolunteerAccessor
	{
		/// <summary>
		/// Oleksiy Fedchuk
		/// Created: 2023/02/24
		/// 
		/// Selects Volunteers by fundrasing event Id
		/// </summary>
		///
		/// <remarks>
		/// Zaid Rachman
		/// Updated: 2023/04/24
		/// 
		/// Final QA
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		List<VolunteerVM> SelectVolunteersbyFundraisingEventId(int fundraisingEventId);
		/// <summary>
		/// Oleksiy Fedchuk
		/// Created: 2023/04/21
		/// 
		/// </summary>
		/// <remarks>
		/// Zaid Rachman
		/// Updated: 2023/04/24
		/// 
		/// Final QA
		/// </remarks>
		/// <returns></returns>
		List<int> SelectAllVolunteers();
		/// <summary>
		/// Oleksiy Fedchuk
		/// Created: 2023/04/21
		/// 
		/// </summary>
		/// <remarks>
		/// Zaid Rachman
		/// Updated: 2023/04/24
		/// 
		/// Final QA
		/// </remarks>
		/// <param name="volunteer"></param>
		/// <param name="fundraisingEventId"></param>
		/// <returns></returns>
		int InsertVolunteerToEventbyVolunteerAndEventId(int userId, int fundraisingEventId);
		/// <summary>
		/// Oleksiy Fedchuk
		/// Created: 2023/04/21
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
		int DeleteVolunteerFromEventbyUsersIdAndFundraisingEventId(int usersId, int fundraisingEventId);
	}
}
