using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CAService" in code, svc and config file together.
public class CAService : ICAService
{

    CommunityAssistEntities cae = new CommunityAssistEntities();


    public List<string> GetCommunityServices()
    {
        var serv = from s in cae.CommunityServices
                   orderby s.ServiceName
                   select new { s.ServiceName };
        List<string> servList = new List<string>();
        foreach(var a in serv)
        {
            servList.Add(a.ServiceName);
        }

        return servList;
    }

    public List<ServiceGrant> GetGrants(string service)
    {
        var grants = from g in cae.ServiceGrants
                     orderby g.GrantDate
                     where g.CommunityService.ServiceName.Equals(service)
                     select new
                     {
                         g.GrantDate,
                         g.GrantAmount,
                         g.ServiceKey,
                         g.GrantApprovalStatus,
                         g.GrantReviewDate,
                         g.GrantAllocation
                     };

        List<ServiceGrant> grantList=new List<ServiceGrant>();

        foreach(var gr in grants)
        {
            ServiceGrant sg = new ServiceGrant();
            sg.GrantDate = gr.GrantDate;
            sg.GrantAmount = gr.GrantAmount;
            sg.ServiceKey = gr.ServiceKey;
            sg.GrantApprovalStatus = gr.GrantApprovalStatus;
            sg.GrantReviewDate = gr.GrantReviewDate;
            sg.GrantAllocation = gr.GrantAllocation;
            grantList.Add(sg);
        }

        return grantList;
    }
}
