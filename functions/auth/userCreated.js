const functions = require('firebase-functions');
const admin = require('firebase-admin')
const moment = require('moment')

module.exports = functions.auth.user().onCreate(async (user, context) => {
    const db = admin.database()
    await db.ref(`users/${user.uid}`).set({
        userName: user.displayName,
        registered: moment().format('DD-MM-YYYY'),
        profilePicture: user.photoURL,
    })
})