import * as vscode from 'vscode';

// Define the equity management agent
export function activate(context: vscode.ExtensionContext) {
    // Register the equity management chat participant
    const participant = vscode.chat.createChatParticipant('equity-manager', handleRequest);
    participant.iconPath = vscode.ThemeIcon.File;
    
    context.subscriptions.push(participant);
    
    // Register the help command
    const helpCommand = vscode.commands.registerCommand('equityManager.help', () => {
        vscode.window.showInformationMessage(
            'Equity Management Copilot Agent is active! Use @equity-manager in chat to get help with equity management tasks.'
        );
    });
    
    context.subscriptions.push(helpCommand);
    
    console.log('Equity Management Copilot Agent is now active!');
}

async function handleRequest(
    request: vscode.ChatRequest,
    context: vscode.ChatContext,
    stream: vscode.ChatResponseStream,
    token: vscode.CancellationToken
): Promise<void> {
    try {
        const prompt = request.prompt.toLowerCase();
        
        // Handle different equity management queries
        if (prompt.includes('valuation') || prompt.includes('value')) {
            await handleValuationQuery(stream);
        } else if (prompt.includes('portfolio') || prompt.includes('allocation')) {
            await handlePortfolioQuery(stream);
        } else if (prompt.includes('risk') || prompt.includes('analysis')) {
            await handleRiskAnalysisQuery(stream);
        } else if (prompt.includes('compliance') || prompt.includes('regulation')) {
            await handleComplianceQuery(stream);
        } else if (prompt.includes('reporting') || prompt.includes('report')) {
            await handleReportingQuery(stream);
        } else if (prompt.includes('help') || prompt === '') {
            await showHelp(stream);
        } else {
            await handleGeneralQuery(stream, request.prompt);
        }
    } catch (error) {
        stream.markdown('❌ Sorry, I encountered an error processing your request. Please try again.');
    }
}

async function handleValuationQuery(stream: vscode.ChatResponseStream): Promise<void> {
    stream.markdown('## 📊 Equity Valuation Assistance\\n\\n');
    stream.markdown('I can help you with:\\n');
    stream.markdown('- **DCF Analysis**: Discounted Cash Flow calculations\\n');
    stream.markdown('- **Comparative Valuation**: P/E, P/B, EV/EBITDA ratios\\n');
    stream.markdown('- **Asset-based Valuation**: Book value and liquidation value\\n');
    stream.markdown('- **Option Pricing**: Black-Scholes and binomial models\\n\\n');
    
    stream.markdown('**Sample Code - DCF Calculation:**\\n');
    stream.markdown('```csharp\\n');
    stream.markdown('public class DCFCalculator\\n');
    stream.markdown('{\\n');
    stream.markdown('    public decimal CalculateNPV(decimal[] cashFlows, decimal discountRate)\\n');
    stream.markdown('    {\\n');
    stream.markdown('        decimal npv = 0;\\n');
    stream.markdown('        for (int i = 0; i < cashFlows.Length; i++)\\n');
    stream.markdown('        {\\n');
    stream.markdown('            npv += cashFlows[i] / (decimal)Math.Pow((double)(1 + discountRate), i + 1);\\n');
    stream.markdown('        }\\n');
    stream.markdown('        return npv;\\n');
    stream.markdown('    }\\n');
    stream.markdown('}\\n');
    stream.markdown('```\\n');
}

async function handlePortfolioQuery(stream: vscode.ChatResponseStream): Promise<void> {
    stream.markdown('## 📈 Portfolio Management Tools\\n\\n');
    stream.markdown('I can assist with:\\n');
    stream.markdown('- **Asset Allocation**: Strategic and tactical allocation\\n');
    stream.markdown('- **Portfolio Optimization**: Mean-variance optimization\\n');
    stream.markdown('- **Rebalancing**: Threshold and calendar-based rebalancing\\n');
    stream.markdown('- **Performance Attribution**: Sector and security analysis\\n\\n');
    
    stream.markdown('**Sample Code - Portfolio Allocation:**\\n');
    stream.markdown('```csharp\\n');
    stream.markdown('public class PortfolioAllocation\\n');
    stream.markdown('{\\n');
    stream.markdown('    public Dictionary<string, decimal> CalculateOptimalWeights(\\n');
    stream.markdown('        Dictionary<string, decimal> expectedReturns,\\n');
    stream.markdown('        decimal[,] covarianceMatrix,\\n');
    stream.markdown('        decimal riskTolerance)\\n');
    stream.markdown('    {\\n');
    stream.markdown('        // Modern Portfolio Theory implementation\\n');
    stream.markdown('        // Returns optimal asset weights\\n');
    stream.markdown('        return new Dictionary<string, decimal>();\\n');
    stream.markdown('    }\\n');
    stream.markdown('}\\n');
    stream.markdown('```\\n');
}

async function handleRiskAnalysisQuery(stream: vscode.ChatResponseStream): Promise<void> {
    stream.markdown('## ⚠️ Risk Analysis & Management\\n\\n');
    stream.markdown('I can help with:\\n');
    stream.markdown('- **VaR Calculation**: Value at Risk using historical and Monte Carlo methods\\n');
    stream.markdown('- **Stress Testing**: Scenario analysis and sensitivity testing\\n');
    stream.markdown('- **Beta Analysis**: Systematic risk measurement\\n');
    stream.markdown('- **Correlation Analysis**: Asset correlation matrices\\n\\n');
    
    stream.markdown('**Sample Code - VaR Calculation:**\\n');
    stream.markdown('```csharp\\n');
    stream.markdown('public class RiskCalculator\\n');
    stream.markdown('{\\n');
    stream.markdown('    public decimal CalculateVaR(decimal[] returns, decimal confidenceLevel)\\n');
    stream.markdown('    {\\n');
    stream.markdown('        Array.Sort(returns);\\n');
    stream.markdown('        int index = (int)((1 - confidenceLevel) * returns.Length);\\n');
    stream.markdown('        return returns[index];\\n');
    stream.markdown('    }\\n');
    stream.markdown('}\\n');
    stream.markdown('```\\n');
}

async function handleComplianceQuery(stream: vscode.ChatResponseStream): Promise<void> {
    stream.markdown('## 📋 Compliance & Regulatory Support\\n\\n');
    stream.markdown('I can assist with:\\n');
    stream.markdown('- **SEC Reporting**: Form 13F, 10-K, 10-Q compliance\\n');
    stream.markdown('- **GDPR/Data Protection**: Client data handling\\n');
    stream.markdown('- **Fiduciary Standards**: Best execution and duty of care\\n');
    stream.markdown('- **Position Limits**: Concentration and exposure limits\\n\\n');
    
    stream.markdown('**Sample Code - Compliance Check:**\\n');
    stream.markdown('```csharp\\n');
    stream.markdown('public class ComplianceChecker\\n');
    stream.markdown('{\\n');
    stream.markdown('    public bool CheckPositionLimits(Portfolio portfolio, decimal maxConcentration)\\n');
    stream.markdown('    {\\n');
    stream.markdown('        foreach (var position in portfolio.Positions)\\n');
    stream.markdown('        {\\n');
    stream.markdown('            if (position.Weight > maxConcentration)\\n');
    stream.markdown('                return false;\\n');
    stream.markdown('        }\\n');
    stream.markdown('        return true;\\n');
    stream.markdown('    }\\n');
    stream.markdown('}\\n');
    stream.markdown('```\\n');
}

async function handleReportingQuery(stream: vscode.ChatResponseStream): Promise<void> {
    stream.markdown('## 📊 Reporting & Analytics\\n\\n');
    stream.markdown('I can help with:\\n');
    stream.markdown('- **Performance Reports**: Time-weighted and money-weighted returns\\n');
    stream.markdown('- **Risk Reports**: VaR, tracking error, and drawdown analysis\\n');
    stream.markdown('- **Attribution Reports**: Sector, security, and factor attribution\\n');
    stream.markdown('- **Client Statements**: Holdings, transactions, and performance\\n\\n');
    
    stream.markdown('**Sample Code - Performance Report:**\\n');
    stream.markdown('```csharp\\n');
    stream.markdown('public class PerformanceReporter\\n');
    stream.markdown('{\\n');
    stream.markdown('    public PerformanceReport GenerateReport(Portfolio portfolio, DateTime startDate, DateTime endDate)\\n');
    stream.markdown('    {\\n');
    stream.markdown('        return new PerformanceReport\\n');
    stream.markdown('        {\\n');
    stream.markdown('            TotalReturn = CalculateTotalReturn(portfolio, startDate, endDate),\\n');
    stream.markdown('            Volatility = CalculateVolatility(portfolio, startDate, endDate),\\n');
    stream.markdown('            SharpeRatio = CalculateSharpeRatio(portfolio, startDate, endDate)\\n');
    stream.markdown('        };\\n');
    stream.markdown('    }\\n');
    stream.markdown('}\\n');
    stream.markdown('```\\n');
}

async function showHelp(stream: vscode.ChatResponseStream): Promise<void> {
    stream.markdown('# 🚀 Equity Management Copilot Agent\\n\\n');
    stream.markdown('Welcome! I\'m your specialized AI assistant for equity management integration.\\n\\n');
    stream.markdown('## 🎯 What I Can Help With:\\n\\n');
    stream.markdown('- **💰 Valuation**: DCF, comparables, and option pricing\\n');
    stream.markdown('- **📈 Portfolio Management**: Allocation, optimization, rebalancing\\n');
    stream.markdown('- **⚠️ Risk Analysis**: VaR, stress testing, correlation analysis\\n');
    stream.markdown('- **📋 Compliance**: Regulatory requirements and limits\\n');
    stream.markdown('- **📊 Reporting**: Performance and risk reports\\n\\n');
    
    stream.markdown('## 🔧 Sample Queries:\\n\\n');
    stream.markdown('- "Help me calculate DCF valuation"\\n');
    stream.markdown('- "Show me portfolio optimization code"\\n');
    stream.markdown('- "How do I calculate Value at Risk?"\\n');
    stream.markdown('- "What are the SEC reporting requirements?"\\n');
    stream.markdown('- "Generate a performance report template"\\n\\n');
    
    stream.markdown('## 🚀 Getting Started:\\n\\n');
    stream.markdown('Just ask me anything about equity management, and I\'ll provide code examples, explanations, and best practices!\\n');
}

async function handleGeneralQuery(stream: vscode.ChatResponseStream, prompt: string): Promise<void> {
    stream.markdown('## 🤔 General Equity Management Query\\n\\n');
    stream.markdown(`I understand you're asking about: "${prompt}"\\n\\n`);
    stream.markdown('Here are some general best practices for equity management integration:\\n\\n');
    stream.markdown('- **Data Quality**: Ensure clean, accurate market data\\n');
    stream.markdown('- **Real-time Processing**: Implement efficient data pipelines\\n');
    stream.markdown('- **Risk Management**: Build robust risk controls\\n');
    stream.markdown('- **Scalability**: Design for high-volume processing\\n');
    stream.markdown('- **Compliance**: Integrate regulatory requirements\\n\\n');
    
    stream.markdown('For more specific help, try asking about:\\n');
    stream.markdown('- Valuation methods\\n');
    stream.markdown('- Portfolio optimization\\n');
    stream.markdown('- Risk analysis\\n');
    stream.markdown('- Compliance requirements\\n');
    stream.markdown('- Reporting solutions\\n');
}

export function deactivate() {
    console.log('Equity Management Copilot Agent deactivated.');
}