# BVS — Bogsy Video Store

Built with ASP.NET Core MVC and Entity Framework Core, backed by a SQL database.

## Features

- Customer library — add and edit customer records
- Video library — add, edit, and delete VCD/DVD titles with stock tracking
- Rental module — rent and return videos with automatic due date and penalty calculation
- Reports — alphabetical video inventory (in/out counts) and per-customer rental reports with CSV export

## Rental Rules

- VCD rentals: ₱25 | DVD rentals: ₱50
- Rental period: 1 to 3 days (set per video)
- Overdue penalty: ₱5 per day after the due date

## Created by

- Melquides III C. Andrade - Intern