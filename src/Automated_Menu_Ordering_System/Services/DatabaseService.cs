using System.Diagnostics;
using System.Diagnostics.Metrics;
using Npgsql;

public class DatabaseService
{
    private readonly NpgsqlConnection _connection;

    public DatabaseService(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new System.ArgumentException("message", nameof(connectionString));
        _connection = new NpgsqlConnection(connectionString);
        OpenConnection();
    }

    public bool IsConnectionOpen()
    {
        return _connection.State == System.Data.ConnectionState.Open;
    }

    public void OpenConnection()
    {
        if (!IsConnectionOpen())
        {
            for (var i = 0; i < 3; i++)
            {
                try
                {
                    _connection.Open();
                    Debug.WriteLine("Connection opened");
                    break;
                }
                catch (Npgsql.PostgresException ex)
                {
                    Debug.WriteLine(ex.MessageText);
                }
            }
        }
        if (!IsConnectionOpen())
            throw new System.Exception("Connection failed to open");
    }

    public NpgsqlConnection GetConnection()
    {
        OpenConnection();
        return _connection;
    }

    public void CloseConnection()
    {
        _connection.Close();
    }

    public NpgsqlDataReader run_query(string query)
    {
        var cmd = new NpgsqlCommand(query, GetConnection());
        return cmd.ExecuteReader();
    }

    public void run_non_query(string query)
    {
        var cmd = new NpgsqlCommand(query, GetConnection());
        cmd.ExecuteNonQuery();
    }

    public static void PrintReader(NpgsqlDataReader reader)
    {
        while (reader.Read())
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                Debug.WriteLine($"{reader.GetName(i)}: {reader[i]}");
            }
        }
    }

    public NpgsqlDataReader get_product_by_category_and_subcategory(string? category, string? subcategory = null)
    {
        string? where_clause;
        if (string.IsNullOrEmpty(category) && string.IsNullOrEmpty(subcategory))
            where_clause = "";
        else if (string.IsNullOrEmpty(subcategory))
            where_clause = $@"WHERE p.category = '{category}'";
        else
            where_clause = $@"WHERE p.category = '{category}' AND p.subcategory = '{subcategory}'";

        var query = $@"
SELECT
    p.id,
    p.name,
    p.description,
    p.image_url,
    p.price,
    p.discount_percent,
    p.estimated_time,
    p.category,
    p.subcategory,
    COALESCE(AVG(po.rating), -1) AS avg_rating,
    CASE
        WHEN i.branch_id IS NULL THEN FALSE
        ELSE TRUE
    END AS is_in_stock
FROM
    Product p
LEFT JOIN PlacedOrder po ON p.id = po.item_id
LEFT JOIN IsOutOfStock i ON p.id = i.product_id
{where_clause}
GROUP BY
    p.id, i.branch_id;
";
        return run_query(query);
    }
    public NpgsqlDataReader get_deals()
    {
        var query = $@"
SELECT
    d.id,
    d.name,
    d.description,
    d.image_url,
    d.price,
    AVG(po.rating) AS avg_rating,
    SUM(p.estimated_time) AS estimated_time
FROM
    Deal d
    LEFT JOIN DealProduct dp ON d.id = dp.deal_id
    LEFT JOIN PlacedOrder po ON dp.product_id = po.item_id
    LEFT JOIN Product p ON dp.product_id = p.id
GROUP BY
    d.id;
";
        return run_query(query);
    }
    public NpgsqlDataReader get_account(string userType, string userId, string userPassword)
    {
        string? where_clause;
        if (userPassword == null)
            where_clause = $@"WHERE a.username = '{userId}'";
        else
            where_clause = $@"WHERE a.password = '{userPassword}' AND a.username = '{userId}'";

        var query = $@"
SELECT
    a.id
FROM
    Account a
{where_clause};
";
        return run_query(query);
    }
    public NpgsqlDataReader get_sittingtables(string? id = null)
    {
        var where_clause = "";
        if (id != null)
            where_clause = $@"WHERE t.id = {id}";

        var query = $@"
SELECT
    t.id,
    t.branch_id
FROM
    SittingTable t
{where_clause};
";
        return run_query(query);
    }
    public NpgsqlDataReader get_manager_by_table_id(string tableId)
    {
        var query = $@"
SELECT
    a.username,
    a.password
FROM
    Account a
JOIN SittingTable st ON a.branch_id = st.branch_id
WHERE
    st.id = {tableId} AND a.acc_type = 'manager';
";
        return run_query(query);
    }
    public void insert_an_order(int tableId, int itemId, int quantity, float totalPrice, int estimatedTime, string description, bool isDeal = false)
    {
        var is_deal = isDeal ? "TRUE" : "FALSE";
        var query = $@"
INSERT INTO PlacedOrder (table_id, item_id, quantity, total_price, estimated_time, description, is_deal)
VALUES ({tableId}, {itemId}, {quantity}, {totalPrice}, {estimatedTime}, '{description}', {is_deal});
";
        run_non_query(query);
    }

    public NpgsqlDataReader get_orders_by_table_id(int tableId)
    {
        var query = $@"
SELECT 
    o.id AS order_id,
    o.item_id,
    o.quantity,
    o.estimated_time,
    o.is_deal,
    o.total_price,
    o.created_at,
    o.description,
    o.is_paid,
    o.status,
    o.rating,
    p.id AS product_id,
    p.name AS product_name,
    p.description AS product_description,
    p.image_url AS product_image_url,
    p.price AS product_price,
    d.id AS deal_id,
    d.name AS deal_name,
    d.description AS deal_description,
    d.image_url AS deal_image_url,
    d.price AS deal_price
FROM
    PlacedOrder o
LEFT JOIN Product p ON o.item_id = p.id AND o.is_deal = FALSE
LEFT JOIN Deal d ON o.item_id = d.id AND o.is_deal = TRUE
WHERE
    o.table_id = {tableId} AND o.status != 'closed';";
        return run_query(query);
    }
    public void pay_bill(int tableId)
    {
        var query = $@"
UPDATE PlacedOrder
SET is_paid = TRUE
WHERE table_id = {tableId};
";
        run_non_query(query);
    }

    public void delete_order(int orderId)
    {
        var query = $@"
DELETE FROM PlacedOrder
WHERE id = {orderId};
";
        run_non_query(query);
    }
    public void rate_order(int orderId, float rating)
    {
        var query = $@"
UPDATE PlacedOrder
SET rating = {rating}
WHERE id = {orderId};
";
        run_non_query(query);
    }
}
