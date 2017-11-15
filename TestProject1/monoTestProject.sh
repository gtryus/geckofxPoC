#!/bin/bash
export LD_LIBRARY_PATH=${PWD}/Firefox
export LD_PRELOAD=${LD_LIBRARY_PATH}/libgeckofix.so
mono TestProject1.exe
