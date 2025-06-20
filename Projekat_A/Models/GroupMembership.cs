using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_A.Models;

public partial class GroupMembership 
{ 
    public int GroupUserId { get; set; }

    public int MemberUserId { get; set; }

    public DateOnly JoinDate { get; set; }

    public DateOnly? LeaveDate { get; set; }

    public virtual Group GroupUser { get; set; } = null!;

    public virtual Member MemberUser { get; set; } = null!;
}
