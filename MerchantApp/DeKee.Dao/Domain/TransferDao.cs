using DeKee.Base.Entities.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Dao.Domain
{
    public class TransferDao : Base.BaseDao
    {
        List<Transfer> GetTransfers(Structs.GetTransferInput input)
        {
            return new List<Transfer>();
        }

        void InserTransfer(Transfer item)
        {

        }
    }
}
