# resources

![](https://github.com/pbryon/resources/workflows/checklinks/badge.svg)

Learning resources I've used across programming languages

# Topics (see topics folder)

- [bash](./topics/bash.md)
- [C#](./topics/C%23.md)
- [DevOps](./topics/devops.md)
- [F#](./topics/F%23.md)
- [git](./topics/git.md)
- [JavaScript](./topics/javascript.md)
- [PowerShell](./topics/PowerShell.md)
- [SVG](./topics/svg.md)
- [Unity3D](./topics/unity3d.md)
- [Visual Studio Extensions](./topics/vs-extensions.md)
- [Visual Studio Code extensions](./topics/vscode-extensions.md)
- [(Web) standards](./topics/standards.md)
- [Xamarin](./topics/xamarin.md)

# Checking dead links

If you don't have .NET Core 2.0 installed, see [the prerequisites](https://docs.microsoft.com/en-us/dotnet/core/install/dependencies).

The `pre-commit.sh` script is a git pre-commit hook. Simply copy it to `.git/hooks/pre-commit` and it will check any changed topic files' links automatically through `test.sh`.

You can also manually run `test.sh` on Linux or MacOS and `test.ps1` on Windows (PowerShell).
