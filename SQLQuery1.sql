CREATE PROCEDURE GetHistoriePaged
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Zestaw 1: Pobrane dane
    SELECT 
        h.ID, 
        h.Imie, 
        h.Nazwisko, 
        h.GrupaID, 
        h.TypAkcji, 
        h.Data
    FROM Historie h
    ORDER BY h.ID
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Zestaw 2: Całkowita liczba rekordów
    SELECT COUNT(*) AS TotalCount FROM Historie;
END;
