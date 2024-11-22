DROP TABLE IF EXISTS PlacedOrder;

DROP TABLE IF EXISTS SittingTable;
DROP TABLE IF EXISTS Account;
DROP TABLE IF EXISTS IsOutOfStock;
DROP TABLE IF EXISTS Branch;

DROP TABLE IF EXISTS DealProduct;
DROP TABLE IF EXISTS Deal;
DROP TABLE IF EXISTS Product;

-- Table to store product details
CREATE TABLE IF NOT EXISTS Product (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    image_url TEXT,
    price DECIMAL(10, 2),
    estimated_time INT,
    category TEXT NOT NULL,
    subcategory TEXT,
    discount_percent DECIMAL(5, 2) DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- Table to store deals (e.g., discounts or offers)
CREATE TABLE IF NOT EXISTS Deal (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    image_url TEXT,
    price DECIMAL(10, 2)
);
-- Junction table to manage many-to-many relationships between Deals and Product
CREATE TABLE IF NOT EXISTS DealProduct (
    deal_id INT REFERENCES Deal(id) ON DELETE CASCADE,
    product_id INT REFERENCES Product(id) ON DELETE CASCADE,
    PRIMARY KEY (deal_id, product_id)
);
-- Table to store branch information
CREATE TABLE IF NOT EXISTS Branch (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);
-- Table to manage user accounts (Admin or Manager)
CREATE TABLE IF NOT EXISTS Account (
    id SERIAL PRIMARY KEY,
    username TEXT UNIQUE NOT NULL,
    password TEXT NOT NULL,
    branch_id INT REFERENCES Branch(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    acc_type TEXT CHECK (acc_type IN ('admin', 'manager')) NOT NULL,
    -- Manager's branch (only managers are associated with a branch)
    CHECK (
        (
            acc_type = 'admin'
            AND branch_id IS NULL
        )
        OR (
            acc_type = 'manager'
            AND branch_id IS NOT NULL
        )
    )
);
-- Table to store branch-specific table for seating
CREATE TABLE IF NOT EXISTS SittingTable (
    id SERIAL PRIMARY KEY,
    branch_id INT REFERENCES Branch(id) ON DELETE CASCADE
);
-- Table to manage order placed by customers
CREATE TABLE IF NOT EXISTS PlacedOrder (
    id SERIAL PRIMARY KEY,
    item_id INT,
    table_id INT REFERENCES SittingTable(id) ON DELETE CASCADE,
    quantity INT NOT NULL DEFAULT 1,
    estimated_time INT,
    is_deal BOOLEAN NOT NULL DEFAULT FALSE,
    total_price DECIMAL(10, 2) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    description TEXT,
    is_paid BOOLEAN NOT NULL DEFAULT FALSE,
    rating DECIMAL(3, 2) CHECK (rating >= 1 AND rating <= 5 OR rating IS NULL),
    status TEXT CHECK (status IN ('in_progress', 'ready', 'completed', 'closed')) NOT NULL DEFAULT 'in_progress'
); 
-- Table to store product availability in branches
CREATE TABLE IF NOT EXISTS IsOutOfStock (
    product_id INT REFERENCES Product(id) ON DELETE CASCADE,
    branch_id INT REFERENCES Branch(id) ON DELETE CASCADE,
    PRIMARY KEY (product_id, branch_id)
);