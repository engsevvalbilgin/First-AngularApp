CREATE TRIGGER UpdateStock
ON Activities
AFTER INSERT
AS
BEGIN
    UPDATE Products
    SET number = number - inserted.ProductAmount
    FROM inserted
    WHERE Products.ProductId = inserted.ProductId
      AND number >= inserted.ProductAmount;
END;
