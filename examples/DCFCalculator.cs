using System;
using System.Collections.Generic;
using System.Linq;

namespace EquityManagement.Valuation
{
    /// <summary>
    /// DCF (Discounted Cash Flow) Calculator for equity valuation
    /// </summary>
    public class DCFCalculator
    {
        /// <summary>
        /// Calculate Net Present Value using discounted cash flows
        /// </summary>
        /// <param name="cashFlows">Array of projected cash flows</param>
        /// <param name="discountRate">Discount rate (WACC)</param>
        /// <returns>Net Present Value</returns>
        public decimal CalculateNPV(decimal[] cashFlows, decimal discountRate)
        {
            decimal npv = 0;
            for (int i = 0; i < cashFlows.Length; i++)
            {
                npv += cashFlows[i] / (decimal)Math.Pow((double)(1 + discountRate), i + 1);
            }
            return npv;
        }

        /// <summary>
        /// Calculate terminal value using Gordon Growth Model
        /// </summary>
        /// <param name="finalCashFlow">Cash flow in final projection year</param>
        /// <param name="growthRate">Long-term growth rate</param>
        /// <param name="discountRate">Discount rate (WACC)</param>
        /// <returns>Terminal value</returns>
        public decimal CalculateTerminalValue(decimal finalCashFlow, decimal growthRate, decimal discountRate)
        {
            return (finalCashFlow * (1 + growthRate)) / (discountRate - growthRate);
        }

        /// <summary>
        /// Calculate enterprise value
        /// </summary>
        /// <param name="projectedCashFlows">Projected free cash flows</param>
        /// <param name="terminalValue">Terminal value</param>
        /// <param name="discountRate">Discount rate (WACC)</param>
        /// <param name="projectionYears">Number of projection years</param>
        /// <returns>Enterprise value</returns>
        public decimal CalculateEnterpriseValue(decimal[] projectedCashFlows, decimal terminalValue, decimal discountRate, int projectionYears)
        {
            decimal pvOfCashFlows = CalculateNPV(projectedCashFlows, discountRate);
            decimal pvOfTerminalValue = terminalValue / (decimal)Math.Pow((double)(1 + discountRate), projectionYears);
            return pvOfCashFlows + pvOfTerminalValue;
        }

        /// <summary>
        /// Calculate equity value from enterprise value
        /// </summary>
        /// <param name="enterpriseValue">Enterprise value</param>
        /// <param name="totalDebt">Total debt</param>
        /// <param name="cash">Cash and cash equivalents</param>
        /// <returns>Equity value</returns>
        public decimal CalculateEquityValue(decimal enterpriseValue, decimal totalDebt, decimal cash)
        {
            return enterpriseValue - totalDebt + cash;
        }

        /// <summary>
        /// Calculate price per share
        /// </summary>
        /// <param name="equityValue">Total equity value</param>
        /// <param name="sharesOutstanding">Number of shares outstanding</param>
        /// <returns>Price per share</returns>
        public decimal CalculatePricePerShare(decimal equityValue, long sharesOutstanding)
        {
            return equityValue / sharesOutstanding;
        }
    }

    /// <summary>
    /// Example usage of DCF Calculator
    /// </summary>
    public class DCFExample
    {
        public static void RunExample()
        {
            var calculator = new DCFCalculator();
            
            // Example: 5-year DCF valuation
            decimal[] projectedCashFlows = { 100, 110, 121, 133, 146 }; // in millions
            decimal discountRate = 0.10m; // 10% WACC
            decimal growthRate = 0.03m; // 3% long-term growth
            decimal totalDebt = 500m; // in millions
            decimal cash = 50m; // in millions
            long sharesOutstanding = 10_000_000; // 10 million shares
            
            // Calculate terminal value
            decimal terminalValue = calculator.CalculateTerminalValue(
                projectedCashFlows.Last(), growthRate, discountRate);
            
            // Calculate enterprise value
            decimal enterpriseValue = calculator.CalculateEnterpriseValue(
                projectedCashFlows, terminalValue, discountRate, projectedCashFlows.Length);
            
            // Calculate equity value
            decimal equityValue = calculator.CalculateEquityValue(
                enterpriseValue, totalDebt, cash);
            
            // Calculate price per share
            decimal pricePerShare = calculator.CalculatePricePerShare(
                equityValue, sharesOutstanding);
            
            Console.WriteLine($"DCF Valuation Results:");
            Console.WriteLine($"Terminal Value: ${terminalValue:N2}M");
            Console.WriteLine($"Enterprise Value: ${enterpriseValue:N2}M");
            Console.WriteLine($"Equity Value: ${equityValue:N2}M");
            Console.WriteLine($"Price Per Share: ${pricePerShare:F2}");
        }
    }
}