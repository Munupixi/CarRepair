using System;
using System.Collections.Generic;

namespace CarRepair.Models;

public partial class Request
{
    public Request(int requestId, int? executorId, int statusId, DateOnly creationDate, string? typeCar, string? modelCar, string? problemDescription, string? clientCompleteName, string? clientPhone, string? executorComment)
    {
        RequestId = requestId;
        ExecutorId = executorId;
        StatusId = statusId;
        CreationDate = creationDate;
        TypeCar = typeCar;
        ModelCar = modelCar;
        ProblemDescription = problemDescription;
        ClientCompleteName = clientCompleteName;
        ClientPhone = clientPhone;
        ExecutorComment = executorComment;
    }

    public int RequestId { get; set; }

    public int? ExecutorId { get; set; }

    public int StatusId { get; set; }

    public DateOnly CreationDate { get; set; }

    public string? TypeCar { get; set; }

    public string? ModelCar { get; set; }

    public string? ProblemDescription { get; set; }

    public string? ClientCompleteName { get; set; }

    public string? ClientPhone { get; set; }

    public string? ExecutorComment { get; set; }

    public virtual User? Executor { get; set; }

    public virtual Status Status { get; set; } = null!;
}
