using FluentMigrator;

namespace ProductManagement.Migration
{
    [Migration(1)]
    public class _0001_Products : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString();
        }
    }
}
