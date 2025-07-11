# Installation Guide - Equity Management Copilot Agent

This guide will help you install and set up the Equity Management Copilot Agent in VS Code.

## Prerequisites

- VS Code 1.85.0 or higher
- Node.js (for development)
- Git

## Installation Steps

### Method 1: Development Installation (Recommended for testing)

1. **Clone the repository:**
   ```bash
   git clone https://github.com/algorythm111/InsightSoftwareHackathon2024.git
   cd InsightSoftwareHackathon2024
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Compile the extension:**
   ```bash
   npm run compile
   ```

4. **Open in VS Code:**
   ```bash
   code .
   ```

5. **Run the extension:**
   - Press `F5` to open a new Extension Development Host window
   - The agent will be available as `@equity-manager` in the chat panel

### Method 2: Package and Install

1. **Install vsce (VS Code Extension Manager):**
   ```bash
   npm install -g vsce
   ```

2. **Package the extension:**
   ```bash
   vsce package
   ```

3. **Install the .vsix file:**
   - In VS Code, go to Extensions view (Ctrl+Shift+X)
   - Click "..." menu and select "Install from VSIX..."
   - Select the generated .vsix file

## Using the Agent

1. **Open Chat Panel:**
   - Press `Ctrl+Shift+I` (Windows/Linux) or `Cmd+Shift+I` (Mac)
   - Or go to View > Chat

2. **Start a conversation:**
   - Type `@equity-manager` followed by your question
   - Example: `@equity-manager help me calculate DCF valuation`

3. **Available commands:**
   - `@equity-manager help` - Show available features
   - `@equity-manager valuation` - Get valuation assistance
   - `@equity-manager portfolio` - Portfolio management help
   - `@equity-manager risk` - Risk analysis tools
   - `@equity-manager compliance` - Regulatory guidance
   - `@equity-manager reporting` - Reporting templates

## Verification

To verify the agent is working:

1. Open the Chat panel in VS Code
2. Type `@equity-manager help`
3. You should see a comprehensive help message with available features

## Troubleshooting

### Agent not appearing in chat
- Ensure VS Code version 1.85.0 or higher
- Check that the extension is compiled: `npm run compile`
- Restart VS Code
- Check the Output panel for any error messages

### Extension not loading
- Verify all dependencies are installed: `npm install`
- Check for TypeScript compilation errors
- Review the VS Code Developer Tools console for errors

### Chat functionality not working
- Ensure the Chat feature is enabled in VS Code
- Try restarting the Extension Development Host
- Check VS Code settings for chat participant permissions

## Development

To modify the agent:

1. Edit files in the `src/` directory
2. Run `npm run compile` to build
3. Press `F5` to test in Extension Development Host
4. Use `npm run watch` for automatic compilation during development

## Support

For issues or questions:
- Check the README.md for detailed documentation
- Review the examples in the `examples/` directory
- Look at the agent manifest in `agent-manifest.json`

## Next Steps

Once installed, explore the agent's capabilities:
- Ask for DCF calculation examples
- Request portfolio optimization code
- Get risk analysis templates
- Learn about compliance requirements
- Generate reporting solutions

The agent is designed to be your specialized assistant for equity management integration tasks!