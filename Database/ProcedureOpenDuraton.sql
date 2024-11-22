USE [Proje]
GO
/****** Object:  StoredProcedure [dbo].[GetOpenDuration]    Script Date: 5.08.2024 15:01:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetOpenDuration]
    @ActivityId INT
AS
BEGIN
    SELECT 
        ActivityId,
        State,
        RecordDate,
        CASE 
            WHEN State = 1 THEN 
                CONVERT(VARCHAR, DATEDIFF(MINUTE, RecordDate, GETDATE()))
            ELSE 
                0
        END AS OpenDuration
    FROM dbo.Activities
    WHERE ActivityId = @ActivityId
END
