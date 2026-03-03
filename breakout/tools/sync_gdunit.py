#!/usr/bin/env python3
import os
import shutil
import subprocess
import sys

BREAKOUT_DIR_NAME = "breakout"
SUBMODULES_DIR_NAME = "addons_submodules"
ADDONS_DIR_NAME = "addons"
ADDONS_DIR_PATH = os.path.join(BREAKOUT_DIR_NAME, ADDONS_DIR_NAME)
GDUNIT_NAME = "gdUnit4"
GDUNIT_SUBMODULE_PATH = os.path.join(SUBMODULES_DIR_NAME, GDUNIT_NAME)
SYMLINK_TARGET_PATH = os.path.join(ADDONS_DIR_PATH, GDUNIT_NAME)
SYMLINK_SOURCE_PATH = os.path.join(
        os.getcwd(),
        SUBMODULES_DIR_NAME,
        GDUNIT_NAME,
        "addons",
        GDUNIT_NAME,
    )

GDUNIT_TESTS_DIR_PATH = os.path.join(SYMLINK_TARGET_PATH, "test")
GDUNIT_TAG = "v6.1.1"

def run_command(command, dir=None):
    print(f"+ {' '.join(command)}")
    if dir:
        print(f"Running in {dir}")

    subprocess.Popen(command, cwd=dir).wait()

def run_piped(first_command, second_command, dir=None):
    print(f"+ {' '.join(first_command)} | {' '.join(second_command)}")
    if dir:
        print(f"Running in {dir}")

    first = subprocess.Popen(first_command, cwd=dir, stdout=subprocess.PIPE)
    second = subprocess.Popen(second_command, cwd=dir, stdin=first.stdout, stdout=subprocess.PIPE)

    stdout, _ = second.communicate()
    print(stdout)

def sync_and_update_submodules():
    run_command(["git", "submodule", "sync"])
    run_command(["git", "submodule", "update", "--init"])

def checkout_gdunit_tag():
    print(f"Checkout gdUnit4 to tag {GDUNIT_TAG}")
    run_command(["git", "checkout", "--quiet", GDUNIT_TAG], GDUNIT_SUBMODULE_PATH)
    run_piped(["git", "status", "--branch"], ["head", "-1"], GDUNIT_SUBMODULE_PATH)

def create_gdunit4_symlink():
    if os.path.exists(SYMLINK_TARGET_PATH):
        print(f"Removing existing symlink: {SYMLINK_TARGET_PATH}")
        os.unlink(SYMLINK_TARGET_PATH)
    else:
        print(f"No symlink found at {SYMLINK_TARGET_PATH}")

    print(f"Creating symlink: {SYMLINK_TARGET_PATH} -> {SYMLINK_SOURCE_PATH}")
    os.makedirs(os.path.dirname(SYMLINK_TARGET_PATH), exist_ok=True)
    os.symlink(SYMLINK_SOURCE_PATH, SYMLINK_TARGET_PATH)

def remove_gdunit4_inner_tests():
    if os.path.isdir(GDUNIT_TESTS_DIR_PATH):
        print("Removing GdUnit4 test directory")
        shutil.rmtree(GDUNIT_TESTS_DIR_PATH)
    else:
        print("GdUnit4 test directory not found")

def main():
    sync_and_update_submodules()
    checkout_gdunit_tag()
    create_gdunit4_symlink()
    remove_gdunit4_inner_tests()

if __name__ == "__main__":
    try:
        main()
    except subprocess.CalledProcessError as e:
        print(f"Command failed with exit code {e.returncode}", file=sys.stderr)
        sys.exit(e.returncode)