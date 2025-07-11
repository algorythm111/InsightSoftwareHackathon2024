using System;
using System.Collections.Generic;
using System.Linq;

namespace EquityManagement.Portfolio
{
    /// <summary>
    /// Portfolio optimization using Modern Portfolio Theory
    /// </summary>
    public class PortfolioOptimizer
    {
        /// <summary>
        /// Represents an asset in the portfolio
        /// </summary>
        public class Asset
        {
            public string Symbol { get; set; }
            public decimal ExpectedReturn { get; set; }
            public decimal Weight { get; set; }
            public decimal Risk { get; set; } // Standard deviation
        }

        /// <summary>
        /// Calculate portfolio expected return
        /// </summary>
        /// <param name="assets">List of assets with weights and expected returns</param>
        /// <returns>Portfolio expected return</returns>
        public decimal CalculatePortfolioReturn(List<Asset> assets)
        {
            return assets.Sum(asset => asset.Weight * asset.ExpectedReturn);
        }

        /// <summary>
        /// Calculate portfolio risk (simplified - assumes zero correlation)
        /// </summary>
        /// <param name="assets">List of assets with weights and risks</param>
        /// <returns>Portfolio risk (standard deviation)</returns>
        public decimal CalculatePortfolioRisk(List<Asset> assets)
        {
            decimal variance = assets.Sum(asset => 
                (asset.Weight * asset.Weight) * (asset.Risk * asset.Risk));
            return (decimal)Math.Sqrt((double)variance);
        }

        /// <summary>
        /// Calculate Sharpe ratio
        /// </summary>
        /// <param name="portfolioReturn">Portfolio expected return</param>
        /// <param name="riskFreeRate">Risk-free rate</param>
        /// <param name="portfolioRisk">Portfolio standard deviation</param>
        /// <returns>Sharpe ratio</returns>
        public decimal CalculateSharpeRatio(decimal portfolioReturn, decimal riskFreeRate, decimal portfolioRisk)
        {
            if (portfolioRisk == 0) return 0;
            return (portfolioReturn - riskFreeRate) / portfolioRisk;
        }

        /// <summary>
        /// Simple equal-weight portfolio allocation
        /// </summary>
        /// <param name="assets">List of assets</param>
        /// <returns>Assets with equal weights</returns>
        public List<Asset> EqualWeightAllocation(List<Asset> assets)
        {
            decimal equalWeight = 1.0m / assets.Count;
            foreach (var asset in assets)
            {
                asset.Weight = equalWeight;
            }
            return assets;
        }

        /// <summary>
        /// Risk parity allocation (simplified)
        /// </summary>
        /// <param name="assets">List of assets</param>
        /// <returns>Assets with risk parity weights</returns>
        public List<Asset> RiskParityAllocation(List<Asset> assets)
        {
            // Simplified risk parity: inverse volatility weights
            decimal totalInverseVolatility = assets.Sum(a => 1 / a.Risk);
            
            foreach (var asset in assets)
            {
                asset.Weight = (1 / asset.Risk) / totalInverseVolatility;
            }
            return assets;
        }

        /// <summary>
        /// Check if portfolio weights sum to 1
        /// </summary>
        /// <param name="assets">List of assets</param>
        /// <returns>True if weights sum to approximately 1</returns>
        public bool ValidateWeights(List<Asset> assets)
        {
            decimal totalWeight = assets.Sum(a => a.Weight);
            return Math.Abs(totalWeight - 1.0m) < 0.001m; // Allow small rounding errors
        }
    }

    /// <summary>
    /// Portfolio rebalancing functionality
    /// </summary>
    public class PortfolioRebalancer
    {
        /// <summary>
        /// Calculate rebalancing trades needed
        /// </summary>
        /// <param name="currentHoldings">Current portfolio holdings</param>
        /// <param name="targetWeights">Target allocation weights</param>
        /// <param name="portfolioValue">Total portfolio value</param>
        /// <returns>Dictionary of symbol to trade amount</returns>
        public Dictionary<string, decimal> CalculateRebalancingTrades(
            Dictionary<string, decimal> currentHoldings,
            Dictionary<string, decimal> targetWeights,
            decimal portfolioValue)
        {
            var trades = new Dictionary<string, decimal>();
            
            foreach (var target in targetWeights)
            {
                string symbol = target.Key;
                decimal targetValue = target.Value * portfolioValue;
                decimal currentValue = currentHoldings.GetValueOrDefault(symbol, 0);
                decimal tradeAmount = targetValue - currentValue;
                
                if (Math.Abs(tradeAmount) > portfolioValue * 0.001m) // 0.1% threshold
                {
                    trades[symbol] = tradeAmount;
                }
            }
            
            return trades;
        }

        /// <summary>
        /// Check if rebalancing is needed based on drift threshold
        /// </summary>
        /// <param name="currentWeights">Current portfolio weights</param>
        /// <param name="targetWeights">Target allocation weights</param>
        /// <param name="driftThreshold">Maximum allowed drift (e.g., 0.05 for 5%)</param>
        /// <returns>True if rebalancing is needed</returns>
        public bool IsRebalancingNeeded(
            Dictionary<string, decimal> currentWeights,
            Dictionary<string, decimal> targetWeights,
            decimal driftThreshold)
        {
            foreach (var target in targetWeights)
            {
                string symbol = target.Key;
                decimal currentWeight = currentWeights.GetValueOrDefault(symbol, 0);
                decimal drift = Math.Abs(currentWeight - target.Value);
                
                if (drift > driftThreshold)
                {
                    return true;
                }
            }
            
            return false;
        }
    }

    /// <summary>
    /// Example usage of portfolio optimization
    /// </summary>
    public class PortfolioExample
    {
        public static void RunExample()
        {
            var optimizer = new PortfolioOptimizer();
            var rebalancer = new PortfolioRebalancer();
            
            // Create sample assets
            var assets = new List<PortfolioOptimizer.Asset>
            {
                new PortfolioOptimizer.Asset { Symbol = "AAPL", ExpectedReturn = 0.12m, Risk = 0.25m },
                new PortfolioOptimizer.Asset { Symbol = "GOOGL", ExpectedReturn = 0.15m, Risk = 0.30m },
                new PortfolioOptimizer.Asset { Symbol = "MSFT", ExpectedReturn = 0.11m, Risk = 0.22m },
                new PortfolioOptimizer.Asset { Symbol = "BOND", ExpectedReturn = 0.04m, Risk = 0.05m }
            };
            
            // Equal weight allocation
            var equalWeightPortfolio = optimizer.EqualWeightAllocation(new List<PortfolioOptimizer.Asset>(assets));
            decimal equalWeightReturn = optimizer.CalculatePortfolioReturn(equalWeightPortfolio);
            decimal equalWeightRisk = optimizer.CalculatePortfolioRisk(equalWeightPortfolio);
            decimal equalWeightSharpe = optimizer.CalculateSharpeRatio(equalWeightReturn, 0.02m, equalWeightRisk);
            
            // Risk parity allocation
            var riskParityPortfolio = optimizer.RiskParityAllocation(new List<PortfolioOptimizer.Asset>(assets));
            decimal riskParityReturn = optimizer.CalculatePortfolioReturn(riskParityPortfolio);
            decimal riskParityRisk = optimizer.CalculatePortfolioRisk(riskParityPortfolio);
            decimal riskParitySharpe = optimizer.CalculateSharpeRatio(riskParityReturn, 0.02m, riskParityRisk);
            
            Console.WriteLine("Portfolio Optimization Results:");
            Console.WriteLine($"\\nEqual Weight Portfolio:");
            Console.WriteLine($"Expected Return: {equalWeightReturn:P2}");
            Console.WriteLine($"Risk: {equalWeightRisk:P2}");
            Console.WriteLine($"Sharpe Ratio: {equalWeightSharpe:F3}");
            
            Console.WriteLine($"\\nRisk Parity Portfolio:");
            Console.WriteLine($"Expected Return: {riskParityReturn:P2}");
            Console.WriteLine($"Risk: {riskParityRisk:P2}");
            Console.WriteLine($"Sharpe Ratio: {riskParitySharpe:F3}");
            
            // Example rebalancing
            var currentHoldings = new Dictionary<string, decimal>
            {
                { "AAPL", 50000m },
                { "GOOGL", 45000m },
                { "MSFT", 35000m },
                { "BOND", 70000m }
            };
            
            var targetWeights = new Dictionary<string, decimal>
            {
                { "AAPL", 0.25m },
                { "GOOGL", 0.25m },
                { "MSFT", 0.25m },
                { "BOND", 0.25m }
            };
            
            decimal portfolioValue = currentHoldings.Values.Sum();
            var trades = rebalancer.CalculateRebalancingTrades(currentHoldings, targetWeights, portfolioValue);
            
            Console.WriteLine($"\\nRebalancing Trades (Portfolio Value: ${portfolioValue:N0}):");
            foreach (var trade in trades)
            {
                string action = trade.Value > 0 ? "BUY" : "SELL";
                Console.WriteLine($"{action} {trade.Key}: ${Math.Abs(trade.Value):N0}");
            }
        }
    }
}