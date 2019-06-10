﻿using FlexLabs.EntityFrameworkCore.Upsert.Runners;

namespace FlexLabs.EntityFrameworkCore.Upsert.Tests.Runners
{
    public class PostgreSqlUpsertCommandRunnerTests : RelationalCommandRunnerTestsBase
    {
        protected override RelationalUpsertCommandRunner GetRunner() => new PostgreSqlUpsertCommandRunner();

        protected override string NoUpdate_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO NOTHING";

        protected override string NoUpdate_WithNullable_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID1\", \"ID2\") " +
            "DO NOTHING";

        protected override string Update_Constant_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Name\" = @p2";

        protected override string Update_Source_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Name\" = EXCLUDED.\"Name\"";

        protected override string Update_Source_RenamedCol_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Name\" = EXCLUDED.\"Name2\"";

        protected override string Update_BinaryAdd_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Status\" = ( \"T\".\"Status\" + @p2 )";

        protected override string Update_Coalesce_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Status\" = ( COALESCE(\"T\".\"Status\", @p2) )";

        protected override string Update_BinaryAddMultiply_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Status\" = ( ( \"T\".\"Status\" + @p2 ) * EXCLUDED.\"Status\" )";

        protected override string Update_BinaryAddMultiplyGroup_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Status\" = ( \"T\".\"Status\" + ( @p2 * EXCLUDED.\"Status\" ) )";

        protected override string Update_Condition_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Name\" = @p2 " +
            "WHERE \"T\".\"Counter\" > @p3";

        protected override string Update_Condition_NullCheck_Sql =>
            "INSERT INTO myTable AS \"T\" (\"Name\", \"Status\") " +
            "VALUES (@p0, @p1) ON CONFLICT (\"ID\") " +
            "DO UPDATE SET \"Name\" = @p2 " +
            "WHERE \"T\".\"Counter\" IS NOT NULL";
    }
}
