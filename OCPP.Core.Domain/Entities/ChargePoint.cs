﻿#nullable disable
using System.ComponentModel.DataAnnotations;
namespace OCPP.Core.Domain.Entities;

public partial class ChargePoint
{
  public ChargePoint() => Transactions = new HashSet<Transaction>();

  public string ChargePointId { get; set; }
  public string Name { get; set; }
  public string Comment { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public string ClientCertThumb { get; set; }

  public virtual ICollection<Transaction> Transactions { get; set; }
}

