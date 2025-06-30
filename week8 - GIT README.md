# ------------ handout 1 --------------

#### **Step 1: Setting up Git Configuration**

* Verify Git installation:

  ```bash
  git --version
  ```
* Configure user ID and email:

  ```bash
  git config --global user.name "Your Name"
  git config --global user.email "your.email@example.com"
  ```
* Check configuration:

  ```bash
  git config --list
  ```

#### **Step 2: Integrating Notepad++ with Git**

* Check if `notepad++` opens in Git Bash.
* Add Notepad++ to environment path if not recognized.
* Set alias for Notepad++:

  ```bash
  alias np='"/c/Program Files/Notepad++/notepad++.exe"'
  ```
* Add line to Notepad++ opened via `np ~/.bashrc`
* Set Notepad++ as default Git editor:

  ```bash
  git config --global core.editor "'C:/Program Files/Notepad++/notepad++.exe' -multiInst -nosession"
  ```
* Verify default editor:

  ```bash
  git config --global -e
  ```

#### **Step 3: Added a File to Git Repository**

* Create a new project and initialize Git repo:

  ```bash
  mkdir GitDemo  
  cd GitDemo  
  git init
  ```
* Create and write to `welcome.txt`:

  ```bash
  echo "Welcome to Git Lab" > welcome.txt
  ```
* Verify file and its content:

  ```bash
  ls  
  cat welcome.txt
  ```
* Check Git status:

  ```bash
  git status
  ```
* Add file to staging:

  ```bash
  git add welcome.txt
  ```
* Commit with multi-line comment (opens Notepad++):

  ```bash
  git commit
  ```
* Push to remote:

  ```bash
  git pull origin master  
  git push origin master
  ```

# ----------- handout 2 ------------

**Q: Explain `.gitignore`**
A: `.gitignore` is a file used to tell Git which files or folders to ignore in a project. Files listed in this file will not be tracked or committed.

**Q: Explain how to ignore unwanted files using `.gitignore`**
A: To ignore unwanted files, list their names or patterns (like `*.log` for all `.log` files or `log/` for a folder) inside the `.gitignore` file. Git will then exclude them from tracking and commits.

# ---------- handout 3 -------------


**Q: Explain branching and merging**
Ans:
**Branching** allows you to diverge from the main code line (like `master`) to develop features independently.
**Merging** integrates changes from one branch into another, typically from a feature branch back into `master`.

---

**Q: Explain creating a branch request in GitLab**
Ans:
A **branch request** in GitLab refers to creating a new branch from the repository, usually done using GitLab UI or via Git CLI and pushed to GitLab.

---

**Q: Explain creating a merge request in GitLab**
Ans:
A **merge request (MR)** in GitLab is a request to merge code from one branch into another. It allows code review, testing, and discussion before integration.

---

**Commands (as per instructions):**

```bash
git branch GitNewBranch
git branch -a
git checkout GitNewBranch
echo "New changes" > file.txt
git add file.txt
git commit -m "Added file in GitNewBranch"
git status
git checkout master
git diff master GitNewBranch
git difftool master GitNewBranch
git merge GitNewBranch
git log --oneline --graph --decorate
git branch -d GitNewBranch
git status
```

# ------------ handout 4 --------------


**Q: Explain how to resolve the conflict during merge**
A:
To resolve a merge conflict:

1. Git marks the conflicting parts in the affected file.
2. Open the file and manually resolve the conflict or use a 3-way merge tool like **P4Merge**.
3. After resolving, add the resolved file and commit the changes.
4. The conflict is considered resolved once committed.


**Code (commands expected):**

```bash
git status
git branch GitWork
echo "<msg>Branch content</msg>" > hello.xml
git status
git add hello.xml
git commit -m "Added hello.xml in GitWork"
git checkout master
echo "<msg>Master content</msg>" > hello.xml
git add hello.xml
git commit -m "Added hello.xml in master"
git log --oneline --graph --decorate --all
git diff master GitWork
git difftool master GitWork
git merge GitWork
# (resolve conflict manually or using 3-way merge tool)
git add hello.xml
git commit -m "Resolved merge conflict"
git status
echo "*.*~" >> .gitignore
git add .gitignore
git commit -m "Added backup pattern to .gitignore"
git branch -a
git branch -d GitWork
git log --oneline --graph --decorate
```

# ---------------- handout 5 -------------

### **Q1: Explain how to clean up and push back to remote Git**

**Answer:**
Cleaning up in Git means ensuring the working directory is free from uncommitted changes or conflicts. This includes checking the current status, committing/stashing any changes, and syncing with the latest updates from the remote repository.
Pushing back refers to sending your local commits to the remote repository using `git push`, ensuring the remote reflects your latest changes.

### **Code (Git Bash commands expected in the doc):**

```bash
git status
git branch -a
git pull origin master
git push origin master
```
