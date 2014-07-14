<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="2.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:a="http://www.w3.org/2005/Atom"
  xmlns:m="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"
  xmlns:d="http://schemas.microsoft.com/ado/2007/08/dataservices">
  <xsl:output method="html" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:template match="/">
    <xsl:for-each select="a:feed/a:entry">
      <div class="package rounded">
        <div class="package-icon">
          <img src="/content/images/package.png" />                             
        </div>
        <div class="title">
          <xsl:element name="a">
            <xsl:attribute name="href">
              <xsl:value-of select="a:content/@src" />
            </xsl:attribute>
            <xsl:value-of select="a:title"/>
          </xsl:element>
        </div>
        <div class="package-delete">
          <xsl:element name="a">
            <xsl:attribute name="href">
              Package/Delete/<xsl:value-of select="a:content/@src" />
            </xsl:attribute>Delete
          </xsl:element>
        </div>
        <ul>
          <li>
            <label>Author</label>
            <xsl:value-of select="a:author/a:name"/>
          </li>
          <xsl:for-each select="m:properties">
            <li>
              <label>Version</label>
              <xsl:value-of select="d:Version"/>
            </li>
            <li>
              <label>Published</label>
              <xsl:call-template name="formatdate">
                <xsl:with-param name="datestr" select="d:Published"/>
              </xsl:call-template>
            </li>
            <li>
              <label>Description</label>
              <em>
                <xsl:value-of select="d:Description"/>
              </em>
            </li>
          </xsl:for-each>
        </ul>
      </div>      
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="formatdate">
    <xsl:param name="datestr" />
    <xsl:copy>
      <xsl:value-of select="concat(substring($datestr, 6, 2),'-',substring($datestr, 9, 2),'-',substring($datestr, 1, 4))" />
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>