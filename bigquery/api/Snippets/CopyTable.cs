// Copyright(c) 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.
//
// [START bigquery_copy_table]

using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using System;

public class BigQueryCopyTable
{
    public void CopyTable(
        string projectId = "your-project-id",
        string destinationDatasetId = "your_dataset_id"
    )
    {
        BigQueryClient client = BigQueryClient.Create(projectId);
        TableReference sourceTableRef = new TableReference()
        {
            TableId = "shakespeare",
            DatasetId = "samples",
            ProjectId = "bigquery-public-data"
        };
        TableReference destinationTableRef = client.GetTableReference(
            destinationDatasetId, "destination_table");
        BigQueryJob job = client.CreateCopyJob(
            sourceTableRef, destinationTableRef)
            .PollUntilCompleted() // Wait for the job to complete.
            .ThrowOnAnyError();

        // Retrieve destination table
        BigQueryTable destinationTable = client.GetTable(destinationTableRef);
        Console.WriteLine(
            $"Copied {destinationTable.Resource.NumRows} rows from table "
            + $"{sourceTableRef.DatasetId}.{sourceTableRef.TableId} "
            + $"to {destinationTable.FullyQualifiedId}."
        );
    }
}
// [END bigquery_copy_table]
