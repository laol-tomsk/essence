import pickledb
import secrets

db = pickledb.load('hex.db', True)

k = 'main'
l = str(secrets.token_hex(16))

db.set(l,k)

print(l)
#81339525504e205983360a4e1f20d014

