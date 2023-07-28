import hmac
import hashlib
import json


def verify_signature(secret, compact_json_payload, signature):
    computed_hash = hmac.new(bytes(secret, 'UTF-8'),
                             bytes(compact_json_payload, 'UTF-8'), hashlib.sha256).hexdigest()
    return hmac.compare_digest(signature, computed_hash)


json_string = '''{
    "monday": "75F",
    "tuesday": "80F"
}'''

# Warning! JSON must be compact. Pretty JSON with spaces will not work.
# First convert the json string to a Python object
payload = json.loads(json_string)

# Then from a Python object to a compact json string
compact_json_payload = json.dumps(payload, separators=(',', ':'))

payload_signature = 'b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151'

secret = 'test-secret'
print(verify_signature(secret, compact_json_payload,
      payload_signature))  # should print true