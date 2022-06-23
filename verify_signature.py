import hmac
import hashlib
import json


def verify_signature(secret, payload, signature):
    computed_hash = hmac.new(bytes(secret, 'UTF-8'),
                             bytes(payload, 'UTF-8'), hashlib.sha256).hexdigest()
    return hmac.compare_digest(signature, computed_hash)


payload = {
    'monday': '75F',
    'tuesday': '80F'
}

payload_signature = 'b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151'

secret = 'test-secret'
payload_json = json.dumps(payload, separators=(',', ':'))
print(verify_signature(secret, payload_json,
      payload_signature))  # should print true
