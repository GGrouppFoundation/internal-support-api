﻿using GGroupp.Infra;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace GGroupp.Internal.Support;

internal sealed partial class CustomerSetSearchFunc : IAsyncValueFunc<CustomerSetFindIn, Result<CustomerSetFindOut, Failure<CustomerSetFindFailureCode>>>
{
    private static readonly ReadOnlyCollection<string> entities;

    static CustomerSetSearchFunc()
        =>
        entities = new(new[] { "account" });

    private readonly IDataverseSearchSupplier dataverseSearchSupplier;

    private CustomerSetSearchFunc(IDataverseSearchSupplier dataverseEntityCreateSupplier)
        =>
        this.dataverseSearchSupplier = dataverseEntityCreateSupplier;

    public static CustomerSetSearchFunc Create(IDataverseSearchSupplier dataverseEntityCreateSupplier)
        =>
        new(
            dataverseEntityCreateSupplier ?? throw new ArgumentNullException(nameof(dataverseEntityCreateSupplier)));

    public partial ValueTask<Result<CustomerSetFindOut, Failure<CustomerSetFindFailureCode>>> InvokeAsync(
        CustomerSetFindIn input, CancellationToken cancellationToken = default);
}