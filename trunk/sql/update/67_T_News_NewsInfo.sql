
UPDATE T_News_NewsInfo
SET vcContent = REPLACE( CAST( vcContent AS NVARCHAR(4000)),'http://www.tcgcms.cn','http://code.tcgcms.cn')