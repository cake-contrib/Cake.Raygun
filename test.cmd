@echo off
rd /s /q tools\Addins\Cake.Raygun
rem .\build.cmd
tools\cake\cake test.cake
