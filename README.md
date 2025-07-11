# Equity Management Copilot Agent

This VS Code extension provides a specialized copilot chat participant for equity management integration tasks.

## Features

🚀 **Specialized AI Assistant** for equity management
- Equity valuation (DCF, comparables, options)
- Portfolio management and optimization  
- Risk analysis and VaR calculations
- Compliance and regulatory guidance
- Performance reporting and analytics

## Usage

1. Install the extension in VS Code
2. Open the Chat panel (Ctrl+Shift+I or Cmd+Shift+I)
3. Type `@equity-manager` followed by your question

### Example Queries:

- `@equity-manager help me calculate DCF valuation`
- `@equity-manager show me portfolio optimization code`
- `@equity-manager how do I calculate Value at Risk?`
- `@equity-manager what are SEC reporting requirements?`
- `@equity-manager generate a performance report template`

## Capabilities

### 💰 Valuation
- DCF (Discounted Cash Flow) analysis
- Comparative valuation (P/E, P/B, EV/EBITDA)
- Asset-based valuation
- Option pricing models

### 📈 Portfolio Management
- Strategic and tactical asset allocation
- Portfolio optimization algorithms
- Rebalancing strategies
- Performance attribution analysis

### ⚠️ Risk Analysis
- Value at Risk (VaR) calculations
- Stress testing and scenario analysis
- Beta and correlation analysis
- Risk-adjusted performance metrics

### 📋 Compliance
- SEC reporting requirements
- Data protection and GDPR compliance
- Fiduciary standards
- Position and concentration limits

### 📊 Reporting
- Performance reports and analytics
- Risk assessment reports
- Client statements and holdings
- Regulatory compliance reports

## Installation

### From Source
1. Clone this repository
2. Run `npm install` to install dependencies
3. Run `npm run compile` to build the extension
4. Press F5 to run the extension in a new Extension Development Host window

### From VSIX Package
1. Package the extension: `vsce package`
2. Install the .vsix file in VS Code

## Development

- TypeScript source code in `src/`
- Build with `npm run compile`
- Watch mode: `npm run watch`

## Requirements

- VS Code 1.85.0 or higher
- Chat panel enabled in VS Code

## License

MIT License - see LICENSE file for details.

## Contributing

Contributions welcome! Please read our contributing guidelines and submit pull requests.
