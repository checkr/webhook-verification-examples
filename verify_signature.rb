require 'openssl'
require 'json'

def verify_signature(secret, payload, signature)
  digest  = OpenSSL::Digest.new('sha256')
  computed_hash = OpenSSL::HMAC.hexdigest(digest, secret, payload)
  OpenSSL.secure_compare(signature, computed_hash)
end
  
payload = {
  monday: '75F',
  tuesday: '80F'
}

payload_signature = 'b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151'

secret = 'test-secret'
puts verify_signature(secret, payload.to_json, payload_signature) # should print true