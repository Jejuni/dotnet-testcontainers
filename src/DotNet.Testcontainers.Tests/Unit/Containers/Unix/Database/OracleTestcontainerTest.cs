﻿namespace DotNet.Testcontainers.Tests.Unit.Containers.Unix.Database
{
  using System.Data;
  using System.Threading.Tasks;
  using DotNet.Testcontainers.Tests.Fixtures;
  using Oracle.ManagedDataAccess.Client;
  using Xunit;

  public class OracleTestcontainerTest : IClassFixture<OracleFixture>
  {
    private readonly OracleFixture oracleFixture;

    public OracleTestcontainerTest(OracleFixture oracleFixture)
    {
      this.oracleFixture = oracleFixture;
    }

    [Fact]
    public void CanQueryContainerUsingProvidedConnectionString()
    {
      Assert.Equal("localhost", this.oracleFixture.OracleTestcontainer.Hostname);
      Assert.Equal("system", this.oracleFixture.OracleTestcontainer.Username);
      Assert.Equal("oracle", this.oracleFixture.OracleTestcontainer.Password);
    }

    [Fact]
    public async Task CanConnectToOracleContainerAndOpenConnection()
    {
      // Given
      await using var connection = new OracleConnection(this.oracleFixture.OracleTestcontainer.ConnectionString);

      // When
      await connection.OpenAsync();

      // Then
      Assert.Equal(ConnectionState.Open, connection.State);
    }
  }
}
