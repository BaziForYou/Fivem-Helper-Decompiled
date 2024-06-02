# Fivem-Helper-Decompiled

This repository contains the decompiled code for Fivem Helper, a tool designed to bypass application block walls using DNS routing. The original tool can be found at [https://fivemhelper.ir/](https://fivemhelper.ir/).

## Disclaimer

I want to make it clear that I am not the creator of this tool. I decompiled and shared it out of curiosity and for educational purposes. I do not intend to fight against the original creators or misuse their work. This repository is simply for fun and exploration. Additionally, I did not create this tool, and I take no responsibility for its security or functionality.

## Purpose of this Repository

The primary reason for creating this repository is to understand and analyze the logic behind the original tool. While the tool works effectively, there are some concerns and potential improvements:

1. **Use of Public Repositories**: The original tool uses a public repository for data updates, which might expose sensitive information. It would be better to create a new account or organization specifically for this purpose.

2. **DNS Routing Logic**: The DNS routing logic implemented in the tool is straightforward yet effective. It allows users to bypass restrictions by dynamically updating DNS settings. However, there are more secure ways to handle this. For instance, the tool could temporarily update the DNS settings for the specific task and then revert them back once the task is complete.

3. **Security Concerns**: The tool logs data using webhooks. Even though error logs are useful, publicly storing webhook logs can lead to abuse. I have censored such information in the shared files, but it remains relatively easy to locate.

I encourage others to review the source code and contribute any suggestions for improving the tool while ensuring user data privacy and security are maintained.

Feel free to explore the code and understand its workings.