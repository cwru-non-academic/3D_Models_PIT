# Repo Specific Details

An app designed to gather psychometric intensity data on 3D models like hands, feet, and other body parts. Currently, only a hand model is supported. Data is saved in a CSV file format and can be modified using different software. Once analysis is complete, a file with the same format can be loaded back into the software for visualization. Curently only inlcudes one hand model but you can add your own 3D models. The software uses the mesh to determine the level of detail that can be used when coloring. Higher mesh count means smaller areas of the model the user can individually color. We recomend a model with a high mesh count and an even mesh count. Traditional models have a varying mesh count to match the detail the model needs in specific areas, so we recommend using 3D modeling software to remesh the model. The current version only supports mouse and keyboard controls. 

# Unity Project Setup (PLEASE READ)

Before you clone the repo, follow these instructions:

1. Install GitHub Desktop, which also installs Git LFS: https://desktop.github.com (if you are using git via command line, install Git LFS: https://git-lfs.com)
2. (for GitHub Desktop) Goto the repo website, click the green Code button, and select “Open with GitHub Desktop” and clone the template project to your local machine
(for git command line) Go to GitHub account settings and authorize your SSH key for SSO URL (https://docs.github.com/en/enterprise-cloud@latest/authentication/authenticating-with-saml-single-sign-on/authorizing-an-ssh-key-for-use-with-saml-single-sign-on) and then clone the final project template

## 5. Modify your GLOBAL .gitconfig
Not everyone on your team may have the same path to UnityYAMLMerge (i.e. different operating systems or install locations). Because of this, we suggest you modify your local config to define the "unityyamlmerge" merge tool that this repository points to. To do this:

1. Reveal hidden files on your operating system
Windows 10: Open the File Explorer application and select View > Options > Change folder and search options and then select the View tab and, in Advanced settings, select Show hidden files, folders, and drives and OK

Windows 11: Open the File Explorer application and select View > Show > Hidden Items

MacOS: Click on Finder and press the keyboard combination Shift + Cmd + . (period key)

2. Find and open your local config file inside of the hidden git folder `.git\config`:
3. Identify your version of unity (e.g. `2021.3.0f1`). This will replace the word `VERSION` in the paths commented below depending on your OS
4. Add the following text to the bottom of the file, subbing in the unitymergetool path:
```bash
[mergetool "unityyamlmerge"]
    trustExitCode = false
    #Replace <path to UnityYAMLMerge> in the next line with the following default locations (may be different depending on your Unity installation location)
    # Installs using the Unity Hub (Default):
    # Win: C:\\Program Files\\Unity\\Hub\\Editor\\VERSION\\Editor\\Data\\Tools\\UnityYAMLMerge.exe
    # MacOS: /Applications/Unity/Hub/Editor/VERSION/Unity.app/Contents/Tools/UnityYAMLMerge
    # Linux: /home/USERNAME/Unity/Hub/Editor/VERSION/Editor/Data/Tools/UnityYAMLMerge
    cmd = '<path to UnityYAMLMerge>' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```
5. Change the Git Config settings to use your local Git config in the menu bar Repository > Repository Settings > Git Config > Use a local Git config > Save.

## 6. Add pre-commit and post-merge scripts
Download the scripts from the link below and paste them into the hidden folder `<your_repo>/.git/hooks/`
- Pre-commit: https://github.com/NYUGameCenter/Unity-Git-Config/blob/master/pre-commit
- Post-commit: https://github.com/NYUGameCenter/Unity-Git-Config/blob/master/post-merge
On MacOS and Linux, the pre-commit and post-merge files need to be made executable. This involves using terminal (Terminal.app on MacOS) to modify the file permissions for these two files using the following commands.
```bash
> cd <local_repo_folder>/.git/hooks
> chmod 755 pre-commit
> chmod 755 post-merge
```
