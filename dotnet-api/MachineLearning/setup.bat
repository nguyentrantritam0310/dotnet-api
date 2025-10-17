@echo off
echo ========================================
echo   Face Recognition System Setup
echo ========================================
echo.

echo Dang kiem tra Python...
python --version >nul 2>&1
if %errorlevel% neq 0 (
    echo Loi: Python khong duoc cai dat hoac khong co trong PATH
    echo Vui long cai dat Python 3.8+ tu https://python.org
    pause
    exit /b 1
)

echo Python da duoc cai dat
echo.

echo Dang cai dat dependencies...
python setup.py

if %errorlevel% neq 0 (
    echo.
    echo Loi trong qua trinh setup!
    pause
    exit /b 1
)

echo.
echo ========================================
echo   Setup hoan tat thanh cong!
echo ========================================
echo.
echo Ban co the bat dau su dung he thong nhan dang khuon mat.
echo.
pause
