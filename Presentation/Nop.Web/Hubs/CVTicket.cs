using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Nop.Web.Models.Customer;

namespace Nop.Web.Hubs
{
    //public class CVTicket : Hub
    //{

    //    static List<CustomerTicket> UsersList = new List<CustomerTicket>();

    //    public void ConnectTickets(int CusId, int NguonVeId, string NgayDi)
    //    {
    //        try
    //        {
    //            var ConnId = Context.ConnectionId;
    //            var ngaydi = Convert.ToDateTime(NgayDi);
    //            var groupid = String.Format("{0}_{1}", NguonVeId, ngaydi.Ticks);
    //            var _cust = UsersList.FirstOrDefault(x => x.CustomerId == CusId);
    //            if(_cust==null)
    //            {
    //                //now add USER to UsersList
    //                UsersList.Add(new CustomerTicket { ConnectionId = ConnId, CustomerId = CusId, GroupId=groupid});
    //                Groups.Add(Context.ConnectionId, groupid);
    //                Clients.Caller.onConnected(groupid);
    //            }
    //            else
    //            {
    //                Clients.Caller.onConnected(_cust.GroupId);
    //            }
    //        }

    //        catch
    //        {
    //            Clients.Caller.ErrorConnection();

    //        }


    //    }

    //    public void NXDatCho(int CusId, int NguonVeId, string NgayDi)
    //    {

    //        if (UsersList.Count != 0)
    //        {
    //            var _cust = (from s in UsersList where (s.CustomerId == CusId) select s).First();

    //            //***** Return to Client *****
    //            Clients.Group(_cust.GroupId).getDatCho();
    //        }

    //    }
        


    //    //--group ***** Receive Request From Client ***** { Whenever User close session then OnDisconneced will be occurs }
    //    public override System.Threading.Tasks.Task OnDisconnected()
    //    {

    //        var item = UsersList.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
    //        if (item != null)
    //        {
    //            UsersList.Remove(item);
    //        }

    //        return base.OnDisconnected();
    //    }
    //}
}