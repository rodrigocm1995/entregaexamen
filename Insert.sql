INSERT INTO Buildings(Building, Available) VALUES 
('Edificio 1', 1),
('Edificio 2', 1),
('Edificio 3', 1)

INSERT INTO Customers(FKBuilding, Customer, Prefix, Available) VALUES
(1, 'Cliente 1', 'C1', 1),
(2, 'Cliente 2', 'C2', 1),
(3, 'Cliente 3', 'C3', 1);

INSERT INTO PartNumbers(FKCustomer, PartNumber, Available) VALUES
(1, 'PARTE-1', 1),
(1, 'PARTE-2', 1),
(2, 'PARTE-3', 1),
(2, 'PARTE-4', 1),
(3, 'PARTE-5', 1),
(3, 'PARTE-6', 1);