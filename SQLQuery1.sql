INSERT INTO PanelAccounts (Id, Login, PasswordHash, PanelType, IsActive, CreatedAt)
VALUES
(NEWID(), 'admin', 'gwbklxLIu1mjYNfVsjYjpyAg3klYWmgQjqY2dR2ma1aKsBxNAowel4qrpt+DpE8+', 'Admin', 1, GETUTCDATE()),
(NEWID(), 'waiter', 'J2+nogLVA4WpidMG1gpDFO3MeCa/B2z0zwz77y8QW7fX26nUCJmbQzzgeFAJ+F0Q', 'Waiter', 1, GETUTCDATE()),
(NEWID(), 'cook', '58d3nczSLDy1tNnuUKEZKzBp1PMUuXrUv9WoHGtWE4f3mnaHmSmxBWlApqGfKC8k', 'Cook', 1, GETUTCDATE()),
(NEWID(), 'bartender', 'c+MmA7AFqIXx2PLmAYIbyYuq5TkRjKh3z3FNXQEcM/QfpXYm6lGuOF4UHHKdJTjO', 'Bartender', 1, GETUTCDATE());