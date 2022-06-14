import hmac
import hashlib
import json 

def verifySignature(secret, payload, signature):
  computedHash = hmac.new(bytes(secret, 'UTF-8'), bytes(payload, 'UTF-8'), hashlib.sha256).hexdigest()
  return hmac.compare_digest(signature, computedHash)

payload = {
  'monday': '75F',
  'tuesday': '80F'
}

payloadSignature = 'b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151'

secret = 'test-secret'
payloadJSON = json.dumps(payload, separators=(',', ':'))
print(verifySignature(secret, payloadJSON, payloadSignature)) # should print true