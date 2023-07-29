require 'openssl'
require 'json'

def verify_signature(secret, compact_json_payload, signature)
  digest  = OpenSSL::Digest.new('sha256')
  computed_hash = OpenSSL::HMAC.hexdigest(digest, secret, compact_json_payload)
  OpenSSL.secure_compare(signature, computed_hash)
end

payload = '{
  "key1": "value1",
  "key2": null
}'
# Warning! JSON must be compact. Pretty JSON with spaces will not work.
compact_json_payload = JSON.parse(payload).to_json

payload_signature = 'x-checkr-signature value'

secret = 'secret key'
puts verify_signature(secret, compact_json_payload, payload_signature) # should print true
