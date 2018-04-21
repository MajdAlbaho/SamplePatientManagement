CREATE VIEW Common.GenderView
AS
SELECT MyData.GenderId,
       MyData.Name
FROM
(
    VALUES
        (0, 'Female'),
        (1, 'Male')
) MyData (GenderId, Name)


