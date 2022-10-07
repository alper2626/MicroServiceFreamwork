db.createUser(
        {
            user: "abasda",
            pwd: "Neslican2626",
            roles: [
                {
                    role: "root",
                    db: "firstMongoDb"
                }
            ]
        }
);