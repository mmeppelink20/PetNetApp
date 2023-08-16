using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IShelterItemTransactionAccessor
    {
		/// <summary>
		/// Your Name
		/// Created: 2023/03/01
		/// 
		/// Selects a list of ShelterItemTransactionVM for the specified shelter
		/// </summary>
		/// <param name="shelterId">The id of the shelter to get records for. Should be the shelter the loged in user works at.</param>
		/// <exception cref="SQLException">Selection failed</exception>
		/// <returns>ShelterItemTransactionVMs for the specified shelter</returns>
		List<ShelterItemTransactionVM> SelcetShelterItemTransactionByShelterId(int shelterId);

		/// <summary>
		/// Nathan Zumsande
		/// Created: 2023/04/14
		/// 
		/// Adds an Item Transaction
		/// </summary>
		/// <param name="transaction"></param>
		/// <exception cref="SQLException">Insert Fails</exception>
		/// <returns>The number of rows edited</returns>
		int InsertItemTransaction(ShelterItemTransaction transaction);
	}
}
