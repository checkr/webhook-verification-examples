const crypto = require('crypto')

const verifySignature = (secret, compactJsonPayload, signature) => {
  const hmac = crypto.createHmac('sha256', secret);
  const computedHash = hmac.update(compactJsonPayload).digest('hex')

  return crypto.timingSafeEqual(Buffer.from(signature), Buffer.from(computedHash))
}

const payload = {
  monday: '75F',
  tuesday: '80F'
}
// Warning! JSON must be compact. Pretty JSON with spaces will not work.
const compactJsonPayload = JSON.stringify(payload)

const payloadSignature = 'b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151'

const secret = 'test-secret'
console.log(verifySignature(secret, compactJsonPayload, payloadSignature)) // should print true