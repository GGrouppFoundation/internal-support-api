﻿using GGroupp.Infra;

namespace GGroupp.Internal.Support;

partial class CustomerSetSearchFunc
{
    public partial ValueTask<Result<CustomerSetSearchOut, Failure<CustomerSetSearchFailureCode>>> InvokeAsync(
        CustomerSetSearchIn input, CancellationToken cancellationToken)
        =>
        AsyncPipeline.Start(
            input ?? throw new ArgumentNullException(nameof(input)),
            cancellationToken)
        .Pipe(
            @in => new DataverseSearchIn(@in.SearchText)
            {
                Entities = entities
            })
        .PipeValue(
            dataverseSearchSupplier.SearchAsync)
        .MapFailure(
            failure =>
            {
                var fall = failure.MapFailureCode(fail => CustomerSetSearchFailureCode.Unknown);
                return fall;
            })
        .MapSuccess(
            MapDataverseSearchOut);

    private static CustomerSetSearchOut MapDataverseSearchOut(DataverseSearchOut dataverseSearchOut)
        =>
        new(
            dataverseSearchOut.Value
            .Select(
                item => new CustomerItemSearchOut(item.ObjectId))
            .ToArray());
}